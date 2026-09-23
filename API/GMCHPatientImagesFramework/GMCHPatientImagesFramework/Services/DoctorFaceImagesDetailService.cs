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
    public class DoctorFaceImagesDetailService : IDoctorFaceImagesDetailService
    {
        private readonly AppSettings _appSettings;
        private IDoctorFaceImagesDetailRepository _repository;

        public DoctorFaceImagesDetailService(IDoctorFaceImagesDetailRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }
        public async Task<ReturnObject<List<DoctorFaceImagesDetailResponseDTO>>> GetAllAsync(DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO)
        {
            var response = await _repository.GetAllAsync(doctorFaceImagesDetailDTO);

            if (response == null || !response.Any())
                throw new AppException($"Records {StringConstants.RecordNotFound}");

            return new ReturnObject<List<DoctorFaceImagesDetailResponseDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
        public async Task<ReturnObject<long>> DeleteAsync(DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO)
        {
            var response = await _repository.DeleteAsync(doctorFaceImagesDetailDTO);

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
