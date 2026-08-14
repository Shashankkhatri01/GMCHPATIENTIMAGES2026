using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class RegistrationRepository : RepositoryBase<RegistrationRequestDTO, RegistrationResponseDTO>, IRegistrationRepository
    {
        //Procedure Route
        public RegistrationRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "UserRegistration_crud";
        }
    } 
}
