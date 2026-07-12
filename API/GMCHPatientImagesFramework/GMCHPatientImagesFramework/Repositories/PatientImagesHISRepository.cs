using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientImagesHISRepository : RepositoryBase<PatientImagesHISDTO, PatientImagesHISDTO>, IPatientImagesHISRepository
    {
        //Procedure Route
        public PatientImagesHISRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientImagesTransactions_HIS";
        }
    } 
}
