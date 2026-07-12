using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IPatientImagesDetailSaveService
    { 
        Task<ReturnObject<long>> InsertAsync(PatientImagesDetailSaveDTO patientImagesDetailSaveDTO); 
    }
}
