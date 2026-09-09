using ConfigurationDtos.DTOs;
using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces 
{
    public interface IDashboardService
    {
        Task<ReturnObject<DashboardResponseDTO>> GetAllAsync(DashboardRequestDTO dashboardRequestDTO);
    }
}
