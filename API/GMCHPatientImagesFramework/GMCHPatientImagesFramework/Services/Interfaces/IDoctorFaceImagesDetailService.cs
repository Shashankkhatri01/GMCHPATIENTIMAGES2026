using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IDoctorFaceImagesDetailService
    { 
        Task<ReturnObject<List<DoctorFaceImagesDetailResponseDTO>>> GetAllAsync(DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO);
        Task<ReturnObject<long>> DeleteAsync(DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO);
    }
}
