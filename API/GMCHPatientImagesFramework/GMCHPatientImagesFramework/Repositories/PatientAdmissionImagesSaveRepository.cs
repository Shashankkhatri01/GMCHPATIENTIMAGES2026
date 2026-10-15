using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientAdmissionImagesSaveRepository : RepositoryBase<PatientAdmissionImagesSaveDTO, PatientAdmissionImagesSaveDTO>, IPatientAdmissionImagesSaveRepository
  {
        public PatientAdmissionImagesSaveRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientAdmissionImagesSave_Curd";
        }

    }
}
