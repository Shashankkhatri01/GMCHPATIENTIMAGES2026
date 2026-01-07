
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMCHPatientImagesDtos.DTOs;

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
