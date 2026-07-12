using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientImagesDetailSaveRepository : RepositoryBase<PatientImagesDetailSaveDTO, PatientImagesDetailSaveDTO>, IPatientImagesDetailSaveRepository
  {
        public PatientImagesDetailSaveRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientImagesDetailSave_Curd";
        }

    }
}
