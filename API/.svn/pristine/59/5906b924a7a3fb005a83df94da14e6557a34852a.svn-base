using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GMCHPatientImagesDtos.DTOs;

namespace GMCHPatientImagesFramework.Services.Interfaces
   
{
    public interface IUserService
    {
        
        //Login 
        Task<ReturnObject<LoginDTO>> LoginAsync(LoginRequestDTO userDto, string ipAddress);
        //Get Data
        Task<ReturnObject<LoginDTO>> GetById(long id);
        //Token
        Task<ReturnObject<LoginDTO>> RefreshTokenAsync(string refreshToken, string ipAddress);
        //Password Change
        Task<ReturnObject<long>> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
        //Forgot Password
        Task<ReturnObject<long>> ForgotPassword(LoginDTO loginDTO);
        //Match OTP
        Task<ReturnObject<long>> MatchOtp(LoginDTO loginDTO);
        //Insert
        Task<ReturnObject<long>> Insert(LoginDTO loginDTO);
        //Update Data
        Task<ReturnObject<long>> Update(LoginDTO loginDTO);
        //Get All Data
        Task<ReturnObject<List<LoginDTO>>> GetAll(LoginDTO loginDTO);




    }
}
