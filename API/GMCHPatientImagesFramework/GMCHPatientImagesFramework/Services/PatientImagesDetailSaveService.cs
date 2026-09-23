using ConfigurationDtos.DTOs;
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
        private readonly IFaceVerificationService _faceVerificationService;

        public PatientImagesDetailSaveService(IPatientImagesDetailSaveRepository repository, IDoctorFaceImagesDetailRepository doctorfaceRepository, IFaceVerificationService faceVerificationService, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
            _doctorfaceRepository = doctorfaceRepository;
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

                //string locationName = await GetLocationName(patientImagesDetailSaveDTO.Latitute, patientImagesDetailSaveDTO.Longitute);

                //face validation
                if (_appSettings.AllowFaceAuthentication)
                {
                    if (patientImagesDetailSaveDTO.DoctorId <= 0)
                        throw new AppException("Please select a valid doctor.");

                    DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO = new DoctorFaceImagesDetailDTO
                    {
                        DoctorId = patientImagesDetailSaveDTO.DoctorId,
                        Mode = "search"
                    };

                    var doctorImages =
                        await _doctorfaceRepository.GetAllAsync(doctorFaceImagesDetailDTO);

                    if (doctorImages == null || !doctorImages.Any())
                    {
                        return new ReturnObject<long>
                        {
                            Message = "No doctor face image is registered.",
                            ReturnValue = -3,
                            Status = true,
                            Success = false
                        };
                    }

                    //var faceVerificationResults =
                    //    new List<(string ImageName, string Status, decimal Similarity, string Message)>();

                    foreach (var patientImage in patientImagesDetailSaveDTO.Images)
                    {
                        double bestSimilarity = 0;
                        long bestDoctorFaceId = 0;
                        string bestDoctorImageName = string.Empty;

                        // ------------------------------------------------
                        // Compare ONE patient image with ALL doctor images
                        // ------------------------------------------------

                        foreach (var doctorImage in doctorImages)
                        {
                            if (string.IsNullOrWhiteSpace(doctorImage.ImageFull))
                                continue;

                            // --------------------------------------------
                            // Doctor physical file
                            // --------------------------------------------

                            string doctorRelativePath =
                                doctorImage.ImageFull.TrimStart('/')
                                                    .Replace('/',
                                                        Path.DirectorySeparatorChar);

                            string doctorPhysicalPath =
                                Path.Combine(
                                    _appSettings.URL,
                                    doctorRelativePath);

                            if (!File.Exists(doctorPhysicalPath))
                                continue;

                            // --------------------------------------------
                            // Doctor image = SOURCE
                            // --------------------------------------------

                            using var doctorStream =
                                File.OpenRead(doctorPhysicalPath);

                            // --------------------------------------------
                            // Patient image = TARGET
                            // --------------------------------------------

                            string patientBase64 =
                                patientImage.ImageFull;

                            // If Base64 contains:
                            // data:image/png;base64,xxxxx
                            if (patientBase64.Contains(","))
                            {
                                patientBase64 =
                                    patientBase64.Substring(
                                        patientBase64.IndexOf(",") + 1);
                            }

                            byte[] patientBytes =
                                Convert.FromBase64String(patientBase64);

                            using var patientStream =
                                new MemoryStream(patientBytes);

                            // --------------------------------------------
                            // AWS comparison
                            // --------------------------------------------

                            var result =
                                await _faceVerificationService.CompareFacesAsync(
                                    doctorStream,
                                    patientStream,
                                    0);

                            // --------------------------------------------
                            // Keep BEST similarity
                            // --------------------------------------------

                            if (result.Similarity.HasValue &&
                                result.Similarity.Value > bestSimilarity)
                            {
                                bestSimilarity =
                                    result.Similarity.Value;

                                bestDoctorFaceId =
                                    doctorImage.DoctorFaceId;

                                bestDoctorImageName =
                                    doctorImage.ImageName;
                            }
                        }

                        // ------------------------------------------------
                        // Apply YOUR threshold
                        // ------------------------------------------------

                        double faceThreshold = _appSettings.FaceSimilarityThreshold;

                        if (bestSimilarity < faceThreshold)
                        {
                            return new ReturnObject<long>
                            {
                                Message =
                                    $"Doctor face verification failed for image: " +
                                    $"{patientImage.ImageName}. " +
                                    $"Best similarity: {bestSimilarity:F2}%.",

                                ReturnValue = -3,
                                Status = true,
                                Success = false
                            };
                        }

                        //faceVerificationResults.Add(
                        //                (
                        //                    patientImage.ImageName,
                        //                    "VERIFIED",
                        //                    (decimal)bestSimilarity,
                        //                    $"Doctor face verified successfully. Similarity: {bestSimilarity:F2}%."
                        //                ));

                        // Optional logging during POC
                        Helper.WriteMsg(
                            $"Face verified. " +
                            $"Patient Image: {patientImage.ImageName}, " +
                            $"DoctorFaceId: {bestDoctorFaceId}, " +
                            $"Reference Image: {bestDoctorImageName}, " +
                            $"Similarity: {bestSimilarity:F2}%");
                    }
                }                    
                //end face validation

                // Create DataTable
                DataTable dt = new DataTable();

                dt.Columns.Add("ImageName", typeof(string));
                dt.Columns.Add("ImageFull", typeof(string));

                //dt.Columns.Add("FaceVerificationStatus", typeof(string));
                //dt.Columns.Add("FaceSimilarity", typeof(decimal));
                //dt.Columns.Add("FaceVerificationMessage", typeof(string));

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

                    //var faceResult =
                    //    faceVerificationResults
                    //        .First(x => x.ImageName == image.ImageName);

                                        dt.Rows.Add(
                                            image.ImageName,
                                            subpath + "/" + fileName//,
                                            //faceResult.Status,
                                            //faceResult.Similarity,
                                            //faceResult.Message
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
    }
}
