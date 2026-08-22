using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Repositories
{
    public class UserRepository : RepositoryBase<LoginDTO, LoginDTO>, IUserRepository
    {
        //Procedure Route
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "GetUserLogin";
        }

        //Login
        public async Task<LoginDTO> GetUserLogin(LoginRequestDTO userDto)
        {
           return await GetDataFromStoredProcedureAsync<LoginRequestDTO, LoginDTO>(ProcedureName, userDto);
       
        }
        //token
        public async Task<long> SaveRefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            return await ExecuteStoredProcedureReturnAsync<RefreshTokenDTO>("SaveUserRefreshToken", refreshTokenDTO);
        }
    } 
}
