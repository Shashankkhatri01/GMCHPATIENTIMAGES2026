using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientImagesRepository : RepositoryBase<PatientImagesDTO, PatientImagesDTO>, IPatientImagesRepository
  {
        public PatientImagesRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientImages_Curd";
        }

    }
}
