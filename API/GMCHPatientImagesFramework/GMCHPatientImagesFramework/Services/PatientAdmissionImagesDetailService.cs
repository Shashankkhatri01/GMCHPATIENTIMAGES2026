using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class PatientAdmissionImagesDetailService : IPatientAdmissionImagesDetailService
    {
        private readonly AppSettings _appSettings;
        private IPatientAdmissionImagesDetailRepository _repository;

        public PatientAdmissionImagesDetailService(IPatientAdmissionImagesDetailRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }
        public async Task<ReturnObject<List<PatientAdmissionImagesDetailResponseDTO>>> GetAllAsync(PatientAdmissionImagesDetailDTO patientAdmissionImagesDetailDTO)
        {
            var response = await _repository.GetAllAsync(patientAdmissionImagesDetailDTO);

            if (response == null || !response.Any())
                throw new AppException($"Records {StringConstants.RecordNotFound}");

            return new ReturnObject<List<PatientAdmissionImagesDetailResponseDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
        public async Task<ReturnObject<long>> DeleteAsync(PatientAdmissionImagesDetailDTO patientAdmissionImagesDetailDTO)
        {
            var response = await _repository.DeleteAsync(patientAdmissionImagesDetailDTO);

            if (response <=0)
                throw new AppException($"Image {StringConstants.DeletionFailed}");

            return new ReturnObject<long>
            {
                Message = $"Image {StringConstants.DeleteSuccess}",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}
