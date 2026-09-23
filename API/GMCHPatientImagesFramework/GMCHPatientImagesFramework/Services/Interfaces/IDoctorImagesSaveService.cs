using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IDoctorImagesSaveService
    { 
        Task<ReturnObject<long>> InsertAsync(DoctorFaceImagesSaveDTO doctorFaceImagesSaveDTO); 
    }
}
