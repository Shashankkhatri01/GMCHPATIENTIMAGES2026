using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/patientimagessms")]
    [ApiController]
    public class WhatsAppAPIController : BaseController
    {
        private IWhatsAppAPIService _whatsAppAPIService;
        public WhatsAppAPIController(IWhatsAppAPIService whatsAppAPIService)
        {
            _whatsAppAPIService = whatsAppAPIService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var response = await _whatsAppAPIService.GetAllAsync();

            if (response.Success)
            {
                int sentCount = 0;

                // 2. Loop through reminders
                foreach (var r in response.ReturnValue)
                {
                    var dto = new WhatsAppAPIRequestDTO
                    {
                        campaignName = WhatsAppAPIConstantsDTO.CampaignNameVaccineDueReminder,
                        destination = r.Destination,
                        userName = WhatsAppAPIConstantsDTO.UserName,
                        templateParams = new List<string> { r.TemplateParams, r.BabyName },
                        source = WhatsAppAPIConstantsDTO.Source,
                        paramsFallbackValue = new Dictionary<string, string>
                        {
                            { "FirstName", "user" }
                        }
                    };

                    var campaigns = new[]
                    {
                        WhatsAppAPIConstantsDTO.CampaignNameVaccineDueReminder,
                        WhatsAppAPIConstantsDTO.CampaignNameVaccineDueReminderHindi
                    };

                    foreach (var campaign in campaigns)
                    {
                        dto.campaignName = campaign;
                        var result = await _whatsAppAPIService.SendWhatsAppMessageAsync(dto);
                        if (result.Success)
                            sentCount++;
                    }
                }

                return Ok(new { TotalReminders = response.ReturnValue.Count, Sent = sentCount });
            }
            else
                return BadRequest(response); // fallback
        }
    }    
}
