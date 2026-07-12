using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
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
    public class PatientImagesDetailService : IPatientImagesDetailService
    {
        private readonly AppSettings _appSettings;
        private IPatientImagesDetailRepository _repository;

        public PatientImagesDetailService(IPatientImagesDetailRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }
        public async Task<ReturnObject<List<PatientImagesDetailDTO>>> GetAllAsync(PatientImagesDetailDTO patientImagesDetailDTO)
        {
            var response = await _repository.GetAllAsync(patientImagesDetailDTO);

            if (response.Count <= 0)
                throw new AppException($"Records {StringConstants.RecordNotFound}");

            return new ReturnObject<List<PatientImagesDetailDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
        public async Task<ReturnObject<bool>> DeleteAsync(long id)
        {
            var response = await _repository.DeleteAsync(id);

            if (!response)
                throw new AppException($"Image {StringConstants.DeletionFailed}");

            return new ReturnObject<bool>
            {
                Message = $"Image {StringConstants.DeleteSuccess}",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}
