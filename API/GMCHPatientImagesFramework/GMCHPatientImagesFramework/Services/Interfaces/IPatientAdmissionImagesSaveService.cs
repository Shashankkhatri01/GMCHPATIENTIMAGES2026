using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IPatientAdmissionImagesSaveService
    { 
        Task<ReturnObject<long>> InsertAsync(PatientAdmissionImagesSaveDTO patientAdmissionImagesSaveDTO); 
    }
}
