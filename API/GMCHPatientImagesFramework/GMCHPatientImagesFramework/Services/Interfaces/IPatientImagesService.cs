using ConfigurationDtos.DTOs;
using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces 
{
    public interface IPatientImagesService
    {
        Task<ReturnObject<List<PatientImagesDTO>>> GetAllAsync(PatientImagesDTO patientImagesDTO);
        Task<ReturnObject<long>> InsertAsync(PatientImagesDTO patientImagesDTO);
        Task<ReturnObject<long>> UpdateAsync(PatientImagesDTO patientImagesDTO);
        Task<ReturnObject<bool>> DeleteAsync(long id);
    }
}
