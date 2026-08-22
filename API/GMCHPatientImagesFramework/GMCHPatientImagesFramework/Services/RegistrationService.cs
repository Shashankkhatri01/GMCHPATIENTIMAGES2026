using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppSettings _appSettings;
        private IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository, IOptions<AppSettings> appSettings)
        {
            _registrationRepository = registrationRepository;
            _appSettings = appSettings.Value;
        }

        //Get All data
        public async Task<ReturnObject<List<RegistrationResponseDTO>>> GetAllAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            var response = await _registrationRepository.GetAllAsync(registrationRequestDTO);

            if (response == null || response.Count <=0)
                throw new AppException($"Users {StringConstants.RecordNotFound}");

            return new ReturnObject<List<RegistrationResponseDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }

        public async Task<ReturnObject<RegistrationResponseDTO>> GetByIdAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            var response = await _registrationRepository.GetByIdAsync(registrationRequestDTO);

            if (response == null)
                throw new AppException($"Users {StringConstants.RecordNotFound}");

            return new ReturnObject<RegistrationResponseDTO>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }

        public async Task<ReturnObject<long>> InsertAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                var response = await _registrationRepository.InsertAsync(registrationRequestDTO);

                if (response == 0)
                    throw new AppException($"Login details {StringConstants.SavedFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Login details {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Login details {StringConstants.SavedSuccess}",
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);
                return new ReturnObject<long>
                {
                    Message = $"Login details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }

        public async Task<ReturnObject<long>> UpdateAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                var response = await _registrationRepository.UpdateAsync(registrationRequestDTO);

                if (response == 0)
                    throw new AppException($"Login details {StringConstants.UpdateFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Login name {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Login details {StringConstants.UpdateSuccess}",
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);
                return new ReturnObject<long>
                {
                    Message = $"Login details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
    }
}

