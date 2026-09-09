using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IConsentService
    {
        //Get All Data
        Task<ReturnObject<List<ConsentResponseDTO>>> GetAllAsync(ConsentRequestDTO consentRequestDTO);
        Task<ReturnObject<long>> InsertAsync(ConsentRequestDTO consentRequestDTO);
        Task<ReturnObject<long>> UpdateAsync(ConsentRequestDTO consentRequestDTO);
    }
}
