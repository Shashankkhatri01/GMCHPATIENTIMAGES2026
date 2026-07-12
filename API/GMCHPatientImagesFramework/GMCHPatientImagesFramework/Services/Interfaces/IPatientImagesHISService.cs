using ConfigurationDtos.DTOs;
using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IPatientImagesHISService
    {
        Task<ReturnObject<long>> InsertAsync(PatientImagesHISDTO patientImagesHISDTO);
        Task<ReturnObject<long>> UpdateAsync(PatientImagesHISDTO patientImagesHISDTO);
        Task<ReturnObject<long>> UpdateStatusAsync(PatientImagesHISDTO patientImagesHISDTO);
    }
}
