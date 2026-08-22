using GMCHPatientImagesDtos.DTOs;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<LoginDTO, LoginDTO>
    {
        //User Login
        Task<LoginDTO> GetUserLogin(LoginRequestDTO userDto);
        //Token
        Task<long> SaveRefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
    }
}
