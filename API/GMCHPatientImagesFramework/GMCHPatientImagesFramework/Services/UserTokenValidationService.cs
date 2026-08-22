using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class UserTokenValidationService : IUserTokenValidationService
    {
        private IUserRepository _repository;
        public UserTokenValidationService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<LoginDTO> ValidateUserTokenAsync(int loginId, int tokenVersion)
        {
            var user = await _repository.GetByIdAsync(loginId);

            if (user == null)
                return null;

            if (!user.IsActive)
                return null;

            if (user.TokenVersion != tokenVersion)
                return null;

            return user;
        }
    }
}
