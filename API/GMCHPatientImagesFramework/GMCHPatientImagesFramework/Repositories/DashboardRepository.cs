using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class DashboardRepository : RepositoryBase<DashboardRequestDTO, DashboardResponseDTO>, IDashboardRepository
  {
        public DashboardRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "DashboardLatest_cr";
        }

    }
}
