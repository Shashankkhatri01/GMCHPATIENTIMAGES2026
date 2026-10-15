using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.FaceVerification;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class PatientImagesDetailSaveService : IPatientImagesDetailSaveService
    {
        private readonly AppSettings _appSettings;
        private IPatientImagesDetailSaveRepository _repository;
        private IDoctorFaceImagesDetailRepository _doctorfaceRepository;
        private IPatientAdmissionImagesDetailRepository _patientAdmissionImagesDetailRepository;
        private readonly IFaceVerificationService _faceVerificationService;

        public PatientImagesDetailSaveService(IPatientImagesDetailSaveRepository repository, IDoctorFaceImagesDetailRepository doctorfaceRepository, IPatientAdmissionImagesDetailRepository patientAdmissionImagesDetailRepository, IFaceVerificationService faceVerificationService, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
            _doctorfaceRepository = doctorfaceRepository;
            _patientAdmissionImagesDetailRepository = patientAdmissionImagesDetailRepository;
            _faceVerificationService = faceVerificationService;
        }

        public async Task<ReturnObject<long>> InsertAsync(PatientImagesDetailSaveDTO patientImagesDetailSaveDTO)
        {
            try
            {
                // Validate
                if (patientImagesDetailSaveDTO.Images == null || !patientImagesDetailSaveDTO.Images.Any())
                    throw new AppException("Please upload at least one image.");

                string locationName = string.Empty;
                List<ImageValidationResultDTO> validationResults = new();

                //string locationName = await GetLocationName(patientImagesDetailSaveDTO.Latitute, patientImagesDetailSaveDTO.Longitute);

                //face validation
                if (_appSettings.AllowFaceAuthentication)
                //&& patientImagesDetailSaveDTO.DoctorId == 1244
                {
                    validationResults =
                await ValidateUploadedImagesAsync(
                    patientImagesDetailSaveDTO);

                    var validationErrors =
                        validationResults
                            .Where(x => !x.IsValid)
                            .SelectMany(x =>
                                x.Errors.Select(error =>
                                    $"Image '{x.ImageName}': {error}"))
                            .ToList();

                    if (validationErrors.Any())
                    {
                        return new ReturnObject<long>
                        {
                            Message =
                                "Image validation failed:\n" +
                                string.Join("\n", validationErrors),

                            ReturnValue = -3,
                            Status = true,
                            Success = false
                        };
                    }
                }                    
                //end face validation

                // Create DataTable
                DataTable dt = new DataTable();

                dt.Columns.Add("ImageName", typeof(string));
                dt.Columns.Add("ImageFull", typeof(string));

                dt.Columns.Add("FaceVerificationStatus", typeof(string));
                dt.Columns.Add("FaceSimilarity", typeof(decimal));
                dt.Columns.Add("FaceVerificationMessage", typeof(string));

                string subpath = "/patientimages";
                string uploadPath = Path.Combine(_appSettings.URL, subpath.TrimStart('/'));

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                foreach (var image in patientImagesDetailSaveDTO.Images)
                {
                    if (string.IsNullOrWhiteSpace(image.ImageFull))
                        continue;

                    byte[] contents = Convert.FromBase64String(image.ImageFull);

                    string fileName = $"patient_{patientImagesDetailSaveDTO.PatientImagesId}_{Guid.NewGuid()}.png";

                    string path = Path.Combine(uploadPath, fileName);

                    File.WriteAllBytes(path, contents);

                    var faceResult = validationResults.FirstOrDefault(
                                    x => x.ImageName == image.ImageName);

                    string verificationStatus = null;
                    decimal? similarity = null;
                    string verificationMessage = null;

                    if (_appSettings.AllowFaceAuthentication && faceResult != null)
                    {
                        verificationStatus = "VERIFIED";

                        // Store the lower score of doctor and patient verification.
                        if (faceResult.DoctorSimilarity.HasValue &&
                            faceResult.PatientSimilarity.HasValue)
                        {
                            similarity = Math.Min(
                                faceResult.DoctorSimilarity.Value,
                                faceResult.PatientSimilarity.Value);
                        }

                        verificationMessage =
                            "Doctor and patient face verification passed.";
                    }

                    dt.Rows.Add(
                        image.ImageName,
                        subpath + "/" + fileName,
                        verificationStatus == null
                            ? (object)DBNull.Value
                            : verificationStatus,
                        similarity.HasValue
                            ? (object)similarity.Value
                            : DBNull.Value,
                        verificationMessage == null
                            ? (object)DBNull.Value
                            : verificationMessage
                    );
                }

                    patientImagesDetailSaveDTO.LocationName = locationName;
                // Only ONE database call
                long response = await _repository.InsertBulkAsync(patientImagesDetailSaveDTO, dt,"@Images","dbo.PatientImagesDetailTVP");

                if (response <= 0)
                    throw new AppException($"Image {StringConstants.SavedFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient is Locked",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"{dt.Rows.Count} Image(s) {StringConstants.SavedSuccess}",
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);
                return new ReturnObject<long>
                {
                    Message = $"Details Exception",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
        public async Task<string> GetLocationName(string latitude, string longitude)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={_appSettings.GoogleAPIKey}";
                var response = await client.GetStringAsync(url);

                var json = JObject.Parse(response);

                var status = json["status"]?.ToString();
                if (status == "OK")
                {
                    var results = json["results"] as JArray;
                    var address = results?[0]?["formatted_address"]?.ToString();
                    return address ?? "Location not found";
                }
                else
                {
                    return $"Error: {status}";
                }
            }
        }

        private async Task<List<ImageValidationResultDTO>>ValidateUploadedImagesAsync(PatientImagesDetailSaveDTO dto)
        {
            var results =
                new List<ImageValidationResultDTO>();

            // ---------------------------------------------------------
            // Validate required IDs
            // ---------------------------------------------------------

            if (dto.DoctorId <= 0)
                throw new AppException(
                    "Please select a valid doctor.");

            if (dto.PatientImagesId <= 0)
                throw new AppException(
                    "PatientImagesId is required for face verification.");


            // ---------------------------------------------------------
            // Get doctor reference images
            // ---------------------------------------------------------

            var doctorRequest =
                new DoctorFaceImagesDetailDTO
                {
                    DoctorId = dto.DoctorId,
                    Mode = "search"
                };

            var doctorImages =
                await _doctorfaceRepository.GetAllAsync(doctorRequest);

            if (doctorImages == null ||
                !doctorImages.Any())
            {
                throw new AppException(
                    "No doctor face image is registered.");
            }

            // 2. Get patient admission reference images once
            // Replace this request/response DTO with the actual types
            // used by your patient admission images repository.
            //var patientRequest = new PatientAdmissionImagesDetailDTO
            //{
            //    PatientImagesId = dto.PatientImagesId,
            //    Mode = "search"
            //};

            var patientAdmissionImages = new List<PatientAdmissionImagesDetailResponseDTO>(); // Placeholder for actual patient admission images
            //var patientAdmissionImages =
            //    await _patientAdmissionImagesDetailRepository
            //        .GetAllAsync(patientRequest);

            //if (patientAdmissionImages == null ||
            //    !patientAdmissionImages.Any())
            //{
            //    throw new AppException(
            //        "No patient admission reference images are available.");
            //}

            // ---------------------------------------------------------
            // 3. Validate every uploaded image
            // ---------------------------------------------------------
            foreach (var uploadedImage in dto.Images)
            {
                var result = await ValidateSingleImageAsync(
                    uploadedImage,
                    doctorImages,
                    patientAdmissionImages);

                results.Add(result);
            }
            return results;
        }

        private async Task<ImageValidationResultDTO>
    ValidateSingleImageAsync(
        PatientImageBulkUploadDTO uploadedImage,
        List<DoctorFaceImagesDetailResponseDTO> doctorImages,
        List<PatientAdmissionImagesDetailResponseDTO> patientImages)
        {
            var result = new ImageValidationResultDTO
            {
                ImageName = uploadedImage.ImageName,
                IsValid = true
            };

            if (string.IsNullOrWhiteSpace(uploadedImage.ImageFull))
            {
                result.IsValid = false;
                result.Errors.Add("Uploaded image is empty.");
                return result;
            }

            byte[] uploadedBytes;

            try
            {
                uploadedBytes = Convert.FromBase64String(uploadedImage.ImageFull);
            }
            catch (FormatException)
            {
                result.IsValid = false;
                result.Errors.Add("Uploaded image has invalid Base64 data.");
                return result;
            }

            // 1. Image quality check: blur, insufficient sharpness, etc.
            //var qualityResult =
            //    await _faceVerificationService.CheckImageQualityAsync(
            //        uploadedBytes);

            //if (!qualityResult.IsValid)
            //{
            //    result.IsValid = false;
            //    result.Errors.Add(qualityResult.Message);
            //    return result;
            //}

            // 2. Face occlusion check: hand, mask, or other obstruction.
            var occlusionResult =
                await _faceVerificationService.CheckFaceOcclusionAsync(
                    uploadedBytes);

            if (!occlusionResult.IsValid)
            {
                result.IsValid = false;
                result.Errors.Add(occlusionResult.Message);
                return result;
            }

            // 3. Doctor face verification
            var doctorResult = await VerifyDoctorFaceAsync(
                uploadedBytes,
                doctorImages);

            result.DoctorVerified = doctorResult.IsVerified;
            result.DoctorSimilarity = doctorResult.Similarity;

            if (!doctorResult.IsVerified)
            {
                result.IsValid = false;
                result.Errors.Add(
                    $"Doctor face not matched. Similarity: " +
                    $"{doctorResult.Similarity:F2}%.");
            }

            // 4. Patient face verification
            //var patientResult = await VerifyPatientFaceAsync(
            //    uploadedBytes,
            //    patientImages);

            //result.PatientVerified = patientResult.IsVerified;
            //result.PatientSimilarity = patientResult.Similarity;

            //if (!patientResult.IsVerified)
            //{
            //    result.IsValid = false;
            //    result.Errors.Add(
            //        $"Patient face not matched. Similarity: " +
            //        $"{patientResult.Similarity:F2}%.");
            //}

            return result;
        }

        private async Task<FaceMatchResultDTO> VerifyDoctorFaceAsync(byte[] patientBytes, List<DoctorFaceImagesDetailResponseDTO> doctorImages)
        {
            double bestSimilarity = 0;
            long bestDoctorFaceId = 0;

            string bestDoctorImageName =
                string.Empty;

            foreach (var doctorImage in doctorImages)
            {
                if (string.IsNullOrWhiteSpace(
                    doctorImage.ImageThumb))
                {
                    continue;
                }

                string doctorRelativePath =
                    doctorImage.ImageThumb
                        .TrimStart('/')
                        .Replace(
                            '/',
                            Path.DirectorySeparatorChar);

                string doctorPhysicalPath =
                    Path.Combine(
                        _appSettings.URL,
                        doctorRelativePath);

    //            Helper.WriteMsg(
    //$"FaceDebug | Type=DoctorReference | " +
    //$"ReferenceId={doctorImage.DoctorFaceId} | " +
    //$"FileExists={File.Exists(doctorPhysicalPath)}");

                if (!File.Exists(doctorPhysicalPath))
                    continue;

                using var doctorStream =
                    File.OpenRead(doctorPhysicalPath);

                using var patientStream =
                    new MemoryStream(patientBytes);

                var response =
                    await _faceVerificationService
                        .CompareFacesAsync(
                            doctorStream,
                            patientStream,
                            0);

    //            Helper.WriteMsg(
    //$"RekognitionDebug | " + response);

                if (response.Similarity.HasValue &&
                    response.Similarity.Value >
                    bestSimilarity)
                {
                    bestSimilarity =
                        response.Similarity.Value;

                    bestDoctorFaceId =
                        doctorImage.DoctorFaceId;

                    bestDoctorImageName =
                        doctorImage.ImageName;
                }
            }


            double threshold =
                _appSettings.FaceSimilarityThreshold;

            return new FaceMatchResultDTO
            {
                IsVerified =
                    bestSimilarity >= threshold,

                Similarity =
                    (decimal)bestSimilarity,

                ReferenceId =
                    bestDoctorFaceId,

                ReferenceImageName =
                    bestDoctorImageName
            };
        }

    //    private async Task<FaceMatchResultDTO>
    //VerifyPatientFaceAsync(
    //    byte[] uploadedBytes,
    //    List<PatientAdmissionImagesDetailResponseDTO> patientImages)
    //    {
    //        double bestSimilarity = 0;
    //        long bestReferenceId = 0;
    //        string bestReferenceImageName = string.Empty;

    //        foreach (var referenceImage in patientImages)
    //        {
    //            if (string.IsNullOrWhiteSpace(referenceImage.ImageThumb))
    //                continue;


    //            string patientRelativePath =
    //                referenceImage.ImageThumb
    //                    .TrimStart('/')
    //                    .Replace(
    //                        '/',
    //                        Path.DirectorySeparatorChar);

    //            string referencePhysicalPath =
    //                Path.Combine(
    //                    _appSettings.URL,
    //                    patientRelativePath);

    ////            Helper.WriteMsg(
    ////$"FaceDebug | Type=PatientReference | " +
    ////$"ReferenceId={referenceImage.PatientAdmissionImageId} | " +
    ////$"FileExists={File.Exists(referencePhysicalPath)}");

    //            if (!File.Exists(referencePhysicalPath))
    //                continue;

    //            using var referenceStream =
    //                File.OpenRead(referencePhysicalPath);

    //            using var uploadedStream =
    //                new MemoryStream(uploadedBytes);

    //            var response =
    //                await _faceVerificationService.CompareFacesAsync(
    //                    referenceStream,
    //                    uploadedStream,
    //                    0);

    ////            Helper.WriteMsg(
    ////$"RekognitionDebug | " + response);

    //            if (response.Similarity.HasValue &&
    //                response.Similarity.Value > bestSimilarity)
    //            {
    //                bestSimilarity = response.Similarity.Value;

    //                // Replace with your actual patient image detail ID
    //                bestReferenceId = referenceImage.PatientAdmissionImageId;

    //                bestReferenceImageName = referenceImage.ImageName;
    //            }
    //        }

    //        double threshold = _appSettings.FaceSimilarityThreshold;

    //        return new FaceMatchResultDTO
    //        {
    //            IsVerified = bestSimilarity >= threshold,
    //            Similarity = (decimal)bestSimilarity,
    //            ReferenceId = bestReferenceId,
    //            ReferenceImageName = bestReferenceImageName
    //        };
    //    }
    }
}
