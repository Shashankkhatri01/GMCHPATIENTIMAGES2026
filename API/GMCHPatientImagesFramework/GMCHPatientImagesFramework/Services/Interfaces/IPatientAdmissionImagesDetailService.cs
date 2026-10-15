using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IPatientAdmissionImagesDetailService
    { 
        Task<ReturnObject<List<PatientAdmissionImagesDetailResponseDTO>>> GetAllAsync(PatientAdmissionImagesDetailDTO patientAdmissionImagesDetailDTO);
        Task<ReturnObject<long>> DeleteAsync(PatientAdmissionImagesDetailDTO patientAdmissionImagesDetailDTO);
    }
}
