
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class LoginService : ILoginService
  {
        private readonly AppSettings _appSettings;
        private ILoginRepository _repository;
        public LoginService(ILoginRepository repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }  
    public async Task<ReturnObject<long>> Login(LoginRequestDTO productDTO)
    {
      ReturnObject<long> retVal = new ReturnObject<long>();

      var response = await _repository.InsertAsync(productDTO);

      if (response == 0)
      {
        retVal.Message = $"{StringConstants.SomethingWentWrong}";
        retVal.ReturnValue = response;
        retVal.Status = false;
        retVal.Success = false;
      }

      else if (response == -2)
      {
        retVal.Message = $"Invalid Username/Password.";
        retVal.ReturnValue = response;
        retVal.Status = false;
        retVal.Success = false; 
      } 
      else
      {
        retVal.Message = $"";
        retVal.ReturnValue = response;
        retVal.Status = true;
        retVal.Success = true;
      }
      return retVal;
    }

  }
}
