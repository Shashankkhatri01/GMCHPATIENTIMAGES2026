using ConfigurationDtos.DTOs;
using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class WhatsAppAPIService : IWhatsAppAPIService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly AppSettings _appSettings;
        private IWhatsAppAPIRepository _whatsAppAPIRepository;

        public WhatsAppAPIService(IWhatsAppAPIRepository whatsAppAPIRepository,HttpClient client, IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _whatsAppAPIRepository = whatsAppAPIRepository;
            _client = client;
            _config = config;
            _appSettings = appSettings.Value;
            _client.BaseAddress = new Uri(_appSettings.WhastAppBaseUrl);
        }
        public async Task<WhatsAppAPIResponseDTO> SendWhatsAppMessageAsync(WhatsAppAPIRequestDTO whatsAppAPIRequestDTO)
        {
            try
            {
                // Provider requires apiKey inside body
                whatsAppAPIRequestDTO.apiKey = _appSettings.WhatsAppAPIKey;
                var jsonBody = JsonConvert.SerializeObject(whatsAppAPIRequestDTO);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("", content);
                var raw = await response.Content.ReadAsStringAsync();

                var result = new WhatsAppAPIResponseDTO
                {
                    Success = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode,
                    Message = response.IsSuccessStatusCode ? "Message sent successfully." : "Failed to send message.",
                    MessageResponse = raw
                };

                //Save log always
                var whatsAppLogRequestDTO = new WhatsAppLogRequestDTO
                {
                    CampaignName = whatsAppAPIRequestDTO.campaignName,
                    Destination = whatsAppAPIRequestDTO.destination,
                    TemplateParams = string.Join(", ",
                        whatsAppAPIRequestDTO.templateParams.Where(x => !string.IsNullOrWhiteSpace(x))),
                    IsDelivered = response.IsSuccessStatusCode ? true : false,
                    StatusCode = (int)response.StatusCode,
                    Message = response.IsSuccessStatusCode ? "Message sent successfully." : "Failed to send message.",
                    RawResponse = raw,
                    UserIdC = 0,
                    Mode = "insert"
                };

                await _whatsAppAPIRepository.InsertAsync(whatsAppLogRequestDTO);

                return result;

            }
            catch (Exception ex)
            {
                var whatsAppLogRequestDTO = new WhatsAppLogRequestDTO
                {
                    CampaignName = whatsAppAPIRequestDTO.campaignName,
                    Destination = whatsAppAPIRequestDTO.destination,
                    TemplateParams = string.Join(", ",
                                whatsAppAPIRequestDTO.templateParams.Where(x => !string.IsNullOrWhiteSpace(x))),
                    IsDelivered = false,
                    StatusCode = 500,
                    Message = "Failed to send message.",
                    RawResponse = ex.ToString(),
                    UserIdC = 0,
                    Mode = "insert"
                };
                await _whatsAppAPIRepository.InsertAsync(whatsAppLogRequestDTO);
                Helper.WriteMsg(ex);
                return new WhatsAppAPIResponseDTO
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.ToString()
                };
            }
        }

        public async Task<ReturnObject<List<WhatsAppLogRequestDTO>>> GetAllAsync()
        {   
            var whatsAppLogRequestDTO = new WhatsAppLogRequestDTO
            {
                Mode = "getall"
            };
            var response = await _whatsAppAPIRepository.GetAllAsync(whatsAppLogRequestDTO);

            if (response.Count <= 0)
                throw new AppException($"Vaccination {StringConstants.RecordNotFound}");

            return new ReturnObject<List<WhatsAppLogRequestDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}

