using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IWhatsAppAPIService
    {
        Task<WhatsAppAPIResponseDTO> SendWhatsAppMessageAsync(WhatsAppAPIRequestDTO whatsAppAPIRequestDTO);
        Task<ReturnObject<List<WhatsAppLogRequestDTO>>> GetAllAsync();
    }
}
