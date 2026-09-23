using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class DoctorImagesSaveRepository : RepositoryBase<DoctorFaceImagesSaveDTO, DoctorFaceImagesSaveDTO>, IDoctorImagesSaveRepository
  {
        public DoctorImagesSaveRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "DoctorFaceImagesSave_Curd";
        }

    }
}
