using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class DischargeDeskRepository : RepositoryBase<DischargeDeskRequestDTO, DischargeDeskFullDetailDTO>, IDischargeDeskRepository
  {
        public DischargeDeskRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "DischargeDesk_cr";
        }

    }
}
