using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private INotificationService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public NotificationController(INotificationService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] NotificationDTO notificationDTO)
        {
            notificationDTO.UserIdC = currentUser.LoginId;
            notificationDTO.Mode = "search";
            var response = await _service.GetAllAsync(notificationDTO);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] NotificationDTO notificationDTO)
        {
            notificationDTO.UserIdC = currentUser.LoginId;
            notificationDTO.Mode = "update";
            var response = await _service.UpdateAsync(notificationDTO);
            return Ok(response);
        }
    }
}
