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
    public class PatientImagesDetailSaveService : IPatientImagesDetailSaveService
    {
        private readonly AppSettings _appSettings;
        private IPatientImagesDetailSaveRepository _repository;

        public PatientImagesDetailSaveService(IPatientImagesDetailSaveRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
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

                // Create DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ImageName", typeof(string));
                dt.Columns.Add("ImageFull", typeof(string));

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

                    dt.Rows.Add(
                        image.ImageName,
                        subpath + "/" + fileName);
                }

                patientImagesDetailSaveDTO.LocationName = locationName;
                // Only ONE database call
                long response = await _repository.InsertBulkAsync(patientImagesDetailSaveDTO, dt,"@Images","dbo.PatientImagesDetailTVP");

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
