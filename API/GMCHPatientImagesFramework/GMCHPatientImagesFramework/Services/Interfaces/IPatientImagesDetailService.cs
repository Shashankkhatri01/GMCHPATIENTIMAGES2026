using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IPatientImagesDetailService
    { 
        Task<ReturnObject<List<PatientImagesDetailDTO>>> GetAllAsync(PatientImagesDetailDTO patientImagesDetailDTO);
        Task<ReturnObject<bool>> DeleteAsync(long id);
    }
}
