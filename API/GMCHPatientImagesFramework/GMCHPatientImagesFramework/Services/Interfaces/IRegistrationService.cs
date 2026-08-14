using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IRegistrationService
    {
        //Get All Data
        Task<ReturnObject<List<RegistrationResponseDTO>>> GetAllAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ReturnObject<long>> InsertAsync(RegistrationRequestDTO registrationRequestDTO);
        Task<ReturnObject<long>> UpdateAsync(RegistrationRequestDTO registrationRequestDTO);
    }
}
