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
                    throw new AppException("Patient ID not passed");

                var response = await _repository.UpdateAsync(dischargeDeskRequestDTO);

                if (string.Equals(
                        dischargeDeskRequestDTO.FinalStatus?.Trim(),
                        "information",
                        StringComparison.OrdinalIgnoreCase) &&
                    (!_appSettings.AllowWhatsApp ||
                     !dischargeDeskRequestDTO.SendNotification ||
                     string.IsNullOrWhiteSpace(dischargeDeskRequestDTO.RemarkId)))
                {
                    return new ReturnObject<long>
                    {
                        Message = $"For Information status, WhatsApp must be enabled, notification must be selected, and a campaign must be provided.",
                        ReturnValue = -1,
                        Status = true,
                        Success = false
                    };
                }

                //send whatsapp message
                if (_appSettings.AllowWhatsApp && dischargeDeskRequestDTO.SendNotification && !string.IsNullOrWhiteSpace(dischargeDeskRequestDTO.RemarkId) && string.Equals(
                        dischargeDeskRequestDTO.FinalStatus?.Trim(),
                        "information",
                        StringComparison.OrdinalIgnoreCase))
                {
                    string campaignName = dischargeDeskRequestDTO.RemarkId.Trim();

                    // Call your API using PatientImagesId + CampaignCode.

                    WhatsAppAPIRequestDTO whatsAppGetParametersDTO = new WhatsAppAPIRequestDTO
                    {
                        TransactionId = dischargeDeskRequestDTO.PatientImagesId,
                        campaignName = campaignName
                    };

                    var campaignData =
                        await _whatsAppService.GetAllParametersAsync(whatsAppGetParametersDTO);

                    if (campaignData == null || !campaignData.Success)
                    {
                        throw new AppException(
                            "Unable to retrieve WhatsApp campaign data.");
                    }

                    var campaignDetails = campaignData.ReturnValue;

                    // Parse campaign-specific template parameters.
                    var templateParams = (campaignDetails.TemplateParams ?? string.Empty)
                        .Split('|')
                        .ToList();

                    if (templateParams.Any(string.IsNullOrWhiteSpace))
                    {
                        throw new AppException(
                            "One or more WhatsApp template parameters are missing.");
                    }

                    var whatsAppAPIRequestDTO = new WhatsAppAPIRequestDTO
                    {
                        destination = campaignDetails.Destination,
                        userName = WhatsAppAPIConstantsDTO.UserName,
                        templateParams = templateParams,
                        source = WhatsAppAPIConstantsDTO.Source,
                        paramsFallbackValue = new Dictionary<string, string> { { "FirstName", "user" } },
                        campaignName = campaignName,
                        TransactionId = dischargeDeskRequestDTO.PatientImagesId                        
                    };
                    await _whatsAppService.SendWhatsAppMessageAsync(whatsAppAPIRequestDTO);
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
