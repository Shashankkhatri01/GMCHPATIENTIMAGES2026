using ConfigurationDtos.DTOs;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientImagesDetailRepository : RepositoryBase<PatientImagesDetailDTO, PatientImagesDetailDTO>, IPatientImagesDetailRepository
  {
        public PatientImagesDetailRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientImagesDetail_Curd";
        }

    }
}
