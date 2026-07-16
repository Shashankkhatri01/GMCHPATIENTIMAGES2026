using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IPatientImagesDetailService
    { 
        Task<ReturnObject<List<PatientImagesDetailResponseDTO>>> GetAllAsync(PatientImagesDetailDTO patientImagesDetailDTO);
        Task<ReturnObject<long>> DeleteAsync(PatientImagesDetailDTO patientImagesDetailDTO);
    }
}
