using ConfigurationDtos.DTOs;
using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class DischargeDeskService : IDischargeDeskService
    {
        private readonly AppSettings _appSettings;
        private IDischargeDeskRepository _repository;
        private readonly IWhatsAppAPIService _whatsAppService;

        public DischargeDeskService(IDischargeDeskRepository repository, IWhatsAppAPIService whatsAppAPIService, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
            _whatsAppService = whatsAppAPIService;
        }

        public async Task<ReturnObject<DischargeDeskResponseDTO>> GetAllAsync(
    DischargeDeskRequestDTO dischargeDeskRequestDTO)
        {
            try
            {
                var response = await _repository
                    .GetMultiResultAsync<DischargeDeskResponseDTO>(dischargeDeskRequestDTO);

                if (response == null || response.Detail == null)
                    throw new AppException($"Records {StringConstants.RecordNotFound}");

                return new ReturnObject<DischargeDeskResponseDTO>
                {
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);

                return new ReturnObject<DischargeDeskResponseDTO>
                {
                    ReturnValue = null,
                    Message = "Details",
                    Status = false,
                    Success = false
                };
            }
        }

        public async Task<ReturnObject<long>> UpdateAsync(DischargeDeskRequestDTO dischargeDeskRequestDTO)
        {
            try
            {
                if (dischargeDeskRequestDTO.PatientImagesId <= 0)
                    throw new AppException("Update ID not passed");

                var response = await _repository.UpdateAsync(dischargeDeskRequestDTO);

                //send whatsapp message
                if (_appSettings.AllowWhatsApp && dischargeDeskRequestDTO.SendNotification)
                {
                    var whatsAppAPIRequestDTO = new WhatsAppAPIRequestDTO
                    {
                        destination = dischargeDeskRequestDTO.MobileNo,
                        userName = WhatsAppAPIConstantsDTO.UserName,
                        templateParams = new List<string> { dischargeDeskRequestDTO.MobileNo, _appSettings.AppURL },
                        source = WhatsAppAPIConstantsDTO.Source,
                        paramsFallbackValue = new Dictionary<string, string> { { "FirstName", "user" } }
                    };

                    var campaigns = new[]
                    {
                        WhatsAppAPIConstantsDTO.CampaignNameManualRegistration,
                        WhatsAppAPIConstantsDTO.CampaignNameGMCHRegistrationHindi
                    };

                    foreach (var campaign in campaigns)
                    {
                        whatsAppAPIRequestDTO.campaignName = campaign;
                        whatsAppAPIRequestDTO.TransactionId = dischargeDeskRequestDTO.PatientImagesId;
                        await _whatsAppService.SendWhatsAppMessageAsync(whatsAppAPIRequestDTO);
                    }
                }

                //end whatsapp message

                if (response == 0)
                    throw new AppException($"Record {StringConstants.UpdateFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient is not Discharged yet (Photos pending)",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Record {StringConstants.UpdateSuccess}",
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
                    Message = $"Details Exception",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
    }
}
