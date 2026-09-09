using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class ConsentRepository : RepositoryBase<ConsentRequestDTO, ConsentResponseDTO>, IConsentRepository
    {
        //Procedure Route
        public ConsentRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "Consent_crud";
        }
    } 
}
