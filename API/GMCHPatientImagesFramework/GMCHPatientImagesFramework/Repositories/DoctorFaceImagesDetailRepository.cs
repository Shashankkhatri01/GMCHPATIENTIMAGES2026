using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class DoctorFaceImagesDetailRepository : RepositoryBase<DoctorFaceImagesDetailDTO, DoctorFaceImagesDetailResponseDTO>, IDoctorFaceImagesDetailRepository 
  {
        public DoctorFaceImagesDetailRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "DoctorFaceImagesDetail_Curd";
        }

    }
}
