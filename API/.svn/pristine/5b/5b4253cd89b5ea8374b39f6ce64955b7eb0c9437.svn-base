
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Utils;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImages.Framework.Utils;

namespace GMCHPatientImagesFramework.Services
{

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private IUserRepository _repository;
        
        private TokenManager _tokenManager;


        public UserService(IUserRepository userRepositorie, IOptions<AppSettings> appSettings)
        {
            _repository = userRepositorie;
           
            _appSettings = appSettings.Value;
           
            _tokenManager = new TokenManager(appSettings.Value);
        }
        //Insert
        public async Task<ReturnObject<long>> Insert(LoginDTO loginDTO)
        {
            
            if (loginDTO.LoginName == null)
                throw new AppException($"Please enter email address!");
            if (loginDTO.MobileNo == null)
                throw new AppException($"Please enter mobile no!");
             

            var response = await _repository.InsertAsync(loginDTO);

            if (response == 0)
                throw new AppException($"User {StringConstants.SavedFailed}");
            else if (response == -2)
                throw new AppException($"User name {StringConstants.AlreadyExists}");
            else if (response == -3)
                throw new AppException($"email address {StringConstants.AlreadyExists}");
            else if (response == -4)
                throw new AppException($"mobile number {StringConstants.AlreadyExists}");

            return new ReturnObject<long>
            {
                Message = $"User {StringConstants.SavedSuccess}",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
        //Update
        public async Task<ReturnObject<long>> Update(LoginDTO loginDTO)
        {
             
            if (loginDTO.LoginName == null)
                throw new AppException($"Please enter email address!");
            if (loginDTO.MobileNo == null)
                throw new AppException($"Please enter mobile no!");
            

            var response = await _repository.UpdateAsync(loginDTO);

            if (response == 0)
                throw new AppException($"User {StringConstants.SavedFailed}");
            else if (response == -2)
                throw new AppException($"User name {StringConstants.AlreadyExists}");
            else if (response == -3)
                throw new AppException($"email address {StringConstants.AlreadyExists}");
            else if (response == -4)
                throw new AppException($"mobile number {StringConstants.AlreadyExists}");

            return new ReturnObject<long>
            {
                Message = $"User {StringConstants.UpdateSuccess}",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }

        //Login
        public async Task<ReturnObject<LoginDTO>> LoginAsync(LoginRequestDTO userDto, string ipAddress)

        {
            ReturnObject<LoginDTO> result = new ReturnObject<LoginDTO>();

            var response = await _repository.GetUserLogin(userDto);

            if (response == null)
                throw new AppException("Invalid Username/Password.");
            if (response.LoginId == -3)
                throw new AppException("Your registration is in process please contact to administrator!");


            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _tokenManager.generateJwtToken(response);
            var refreshToken = _tokenManager.generateRefreshToken(ipAddress);

            response.Token = jwtToken;
            response.RefreshToken = refreshToken.Token;

            //Save refresh token in database.
            refreshToken.LoginId = response.LoginId;
            
            refreshToken.NewToken = refreshToken.Token;
            refreshToken.Mode = "new";
            var tokenResponse = await _repository.SaveRefreshTokenAsync(refreshToken);

            if (tokenResponse == -2 || tokenResponse == 0)
                throw new UnauthorizedException("Login Issue.");

            result.Success = true;
            result.Status = true;
            result.ReturnValue = response;
            result.Message = "Login Successful!";
            return result;
        }

        //Token
        public async Task<ReturnObject<LoginDTO>> RefreshTokenAsync(string token, string ipAddress)
        {
            ReturnObject<LoginDTO> result = new ReturnObject<LoginDTO>();
            result.ReturnValue = new LoginDTO();
            var newRefreshToken = _tokenManager.generateRefreshToken(ipAddress);
            newRefreshToken.NewToken = newRefreshToken.Token;
            newRefreshToken.Token = token;
            newRefreshToken.Mode = "existing";
            var tokenResponse = await _repository.SaveRefreshTokenAsync(newRefreshToken);

            if (tokenResponse == -2 || tokenResponse == 0)
                throw new UnauthorizedException(StringConstants.LoginIssue);

            LoginDTO loginDTO = new  LoginDTO ();
            loginDTO.LoginId = Convert.ToInt32(tokenResponse);
            loginDTO.Mode = "getById";

      LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
      loginRequestDTO.LoginId = Convert.ToInt32(tokenResponse);
      loginRequestDTO.Mode = "getById";

      var response = await _repository.GetUserLogin(loginRequestDTO);

            LoginDTO userDTO = new  LoginDTO 
            {
                LoginId = response.LoginId,
                LoginName = response.LoginName,
                
            };
            var jwtToken = _tokenManager.generateJwtToken(userDTO);

            result.ReturnValue.LoginId = userDTO.LoginId;
            result.ReturnValue.LoginName = userDTO.LoginName;
            result.ReturnValue.Token = jwtToken;
            result.ReturnValue.RefreshToken = newRefreshToken.NewToken;

            result.Success = true;
            return result;
        }

        //Password Change
        public async Task<ReturnObject<long>> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        { 
            var response = await _repository.UpdateAsync<ChangePasswordDTO>(changePasswordDTO); 
            if (response <= 0)
                throw new AppException("Invalid Password.");

            return new ReturnObject<long>
            {
                Message = "Password changed successfully!",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }

        //ForgotPassword
        public async Task<ReturnObject<long>> ForgotPassword(LoginDTO loginDTO)
        {
            ReturnObject<long> result = new ReturnObject<long>();
            //Generat Otp 
            var response = await _repository.UpdateAsync(loginDTO);
            if (response == -2)
                throw new AppException($"Please fill valid mobile number!");
            result.Message = $"OTP generated sucessfully";
            result.Success = true;
            result.ReturnValue = response;

            return result;
        }

        //Match Otp
        public async Task<ReturnObject<long>> MatchOtp(LoginDTO loginDTO)
        {
            ReturnObject<long> result = new ReturnObject<long>();
            var response = await _repository.UpdateAsync(loginDTO);
            if (response == 0)
                throw new AppException($"Please fill valid OTP!");
            result.Message = $" OTP verified sucessfully";
            result.Success = true;
            result.ReturnValue = response;

            return result;
        }
        //Edit
        public async Task<ReturnObject<LoginDTO>> GetById(long id)
        {
            var response = await _repository.GetByIdAsync(id);

            if (response == null)
                throw new AppException($"User {StringConstants.RecordNotFound}");

            return new ReturnObject<LoginDTO>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
        //Get All data
        public async Task<ReturnObject<List<LoginDTO>>> GetAll(LoginDTO loginDTO)
        {
            var response = await _repository.GetAllAsync(loginDTO);

            if (response.Count <= 0)
                throw new AppException($"User {StringConstants.RecordNotFound}");

            return new ReturnObject<List<LoginDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}

