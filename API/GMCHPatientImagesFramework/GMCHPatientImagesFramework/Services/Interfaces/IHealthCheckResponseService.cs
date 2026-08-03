using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces 
{
    public interface IHealthCheckResponseService
    {
        Task<ReturnObject<HealthCheckResponseDTO>> GetHealthAsync();
    }
}
