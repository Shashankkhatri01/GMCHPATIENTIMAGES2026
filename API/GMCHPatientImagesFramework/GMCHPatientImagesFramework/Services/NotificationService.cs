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
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppSettings _appSettings;
        private INotificationRepository _repository;
        private readonly IWhatsAppAPIService _whatsAppService;

        public NotificationService(INotificationRepository repository, IWhatsAppAPIService whatsAppAPIService, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
            _whatsAppService = whatsAppAPIService;
        }

        public async Task<ReturnObject<List<NotificationDTO>>> GetAllAsync(
    NotificationDTO notificationDTO)
        {
            var response = await _repository.GetAllAsync(notificationDTO);

            if (response.Count <= 0)
                throw new AppException($"Records {StringConstants.RecordNotFound}");

            return new ReturnObject<List<NotificationDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }

        public async Task<ReturnObject<long>> UpdateAsync(NotificationDTO notificationDTO)
        {
            try
            {
                if (notificationDTO.PatientImagesId <= 0)
                    throw new AppException("Update ID not passed");

                var response = await _repository.UpdateAsync(notificationDTO);

                //send whatsapp message
                //if (_appSettings.AllowWhatsApp && notificationDTO.SendNotification)
                //{
                //    var whatsAppAPIRequestDTO = new WhatsAppAPIRequestDTO
                //    {
                //        destination = notificationDTO.MobileNo,
                //        userName = WhatsAppAPIConstantsDTO.UserName,
                //        templateParams = new List<string> { notificationDTO.MobileNo, _appSettings.AppURL },
                //        source = WhatsAppAPIConstantsDTO.Source,
                //        paramsFallbackValue = new Dictionary<string, string> { { "FirstName", "user" } }
                //    };

                //    var campaigns = new[]
                //    {
                //        WhatsAppAPIConstantsDTO.CampaignNameManualRegistration,
                //        WhatsAppAPIConstantsDTO.CampaignNameGMCHRegistrationHindi
                //    };

                //    foreach (var campaign in campaigns)
                //    {
                //        whatsAppAPIRequestDTO.campaignName = campaign;
                //        whatsAppAPIRequestDTO.TransactionId = notificationDTO.PatientImagesId;
                //        await _whatsAppService.SendWhatsAppMessageAsync(whatsAppAPIRequestDTO);
                //    }
                //}

                //end whatsapp message

                if (response == 0)
                    throw new AppException($"Record {StringConstants.UpdateFailed}");

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
