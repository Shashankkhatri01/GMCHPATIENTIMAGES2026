using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{

    public interface IUserTokenValidationService
    {
        Task<LoginDTO> ValidateUserTokenAsync(int loginId, int tokenVersion);
    }
}
