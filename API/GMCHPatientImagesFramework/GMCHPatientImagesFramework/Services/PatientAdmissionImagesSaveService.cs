using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class PatientAdmissionImagesSaveService : IPatientAdmissionImagesSaveService
    {
        private readonly AppSettings _appSettings;
        private IPatientAdmissionImagesSaveRepository _repository;

        public PatientAdmissionImagesSaveService(IPatientAdmissionImagesSaveRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }

        public async Task<ReturnObject<long>> InsertAsync(PatientAdmissionImagesSaveDTO patientAdmissionImagesSaveDTO)
        {
            try
            {
                // Validate
                if (patientAdmissionImagesSaveDTO.Images == null || !patientAdmissionImagesSaveDTO.Images.Any())
                    throw new AppException("Please upload at least one image.");

                // Create DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ImageName", typeof(string));
                dt.Columns.Add("ImageFull", typeof(string));
                dt.Columns.Add("ContentType", typeof(string));
                dt.Columns.Add("FileSize", typeof(long));

                string subpath = "/patientadmissionimages";
                string uploadPath = Path.Combine(_appSettings.URL, subpath.TrimStart('/'));

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                foreach (var image in patientAdmissionImagesSaveDTO.Images)
                {
                    if (string.IsNullOrWhiteSpace(image.ImageFull))
                        continue;

                    string base64 = image.ImageFull;
                    if (base64.Contains(","))
                    {
                        base64 = base64.Substring(
                            base64.IndexOf(",") + 1);
                    }

                    byte[] contents;
                    try
                    {
                        contents = Convert.FromBase64String(base64);
                    }
                    catch
                    {
                        throw new AppException(
                            $"Invalid base64 image: {image.ImageName}");
                    }

                    string extension = ".jpg";

                    switch (image.ContentType?.ToLower())
                    {
                        case "image/png":
                            extension = ".png";
                            break;

                        case "image/jpeg":
                        case "image/jpg":
                            extension = ".jpg";
                            break;

                        case "image/webp":
                            extension = ".webp";
                            break;
                    }

                    string fileName =
                            $"patient_{patientAdmissionImagesSaveDTO.PatientImagesId}_{Guid.NewGuid()}{extension}";

                    string path = Path.Combine(uploadPath, fileName);

                    //File.WriteAllBytes(path, contents);
                    await File.WriteAllBytesAsync(
                            path,
                            contents);

                    dt.Rows.Add(
                            image.ImageName,
                            subpath + "/" + fileName,
                            image.ContentType,
                            contents.LongLength);
                }

                // Only ONE database call
                long response = await _repository.InsertBulkAsync(patientAdmissionImagesSaveDTO, dt,"@Images", "dbo.PatientAdmissionImagesDetailTVP");

                if (response <= 0)
                    throw new AppException($"Image {StringConstants.SavedFailed}");

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
