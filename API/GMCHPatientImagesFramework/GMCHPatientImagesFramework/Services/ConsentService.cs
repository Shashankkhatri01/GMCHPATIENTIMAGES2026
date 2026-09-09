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
    public class ConsentService : IConsentService
    {
        private readonly AppSettings _appSettings;
        private IConsentRepository _consentRepository;

        public ConsentService(IConsentRepository consentRepository, IOptions<AppSettings> appSettings)
        {
            _consentRepository = consentRepository;
            _appSettings = appSettings.Value;
        }

        //Get All data
        public async Task<ReturnObject<List<ConsentResponseDTO>>> GetAllAsync(ConsentRequestDTO consentRequestDTO)
        {
            var response = await _consentRepository.GetAllAsync(consentRequestDTO);

            if (response == null || response.Count <=0)
                throw new AppException($"Consent {StringConstants.RecordNotFound}");

            return new ReturnObject<List<ConsentResponseDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }

        public async Task<ReturnObject<long>> InsertAsync(ConsentRequestDTO consentRequestDTO)
        {
            try
            {
                var response = await _consentRepository.InsertAsync(consentRequestDTO);

                if (response == 0)
                    throw new AppException($"Consent details {StringConstants.SavedFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Consent details {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Consent details {StringConstants.SavedSuccess}",
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
                    Message = $"Consent details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }

        public async Task<ReturnObject<long>> UpdateAsync(ConsentRequestDTO consentRequestDTO)
        {
            try
            {
                var response = await _consentRepository.UpdateAsync(consentRequestDTO);

                if (response == 0)
                    throw new AppException($"Consent details {StringConstants.UpdateFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Consent name {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Consent details {StringConstants.UpdateSuccess}",
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
                    Message = $"Consent details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
    }
}

