using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientAdmissionImagesDetailRepository : RepositoryBase<PatientAdmissionImagesDetailDTO, PatientAdmissionImagesDetailResponseDTO>, IPatientAdmissionImagesDetailRepository 
  {
        public PatientAdmissionImagesDetailRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientAdmissionImagesDetail_Curd";
        }

    }
}
