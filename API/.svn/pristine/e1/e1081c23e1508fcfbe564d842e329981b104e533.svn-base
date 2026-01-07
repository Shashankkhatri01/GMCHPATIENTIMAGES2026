
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using System.Data;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;

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
