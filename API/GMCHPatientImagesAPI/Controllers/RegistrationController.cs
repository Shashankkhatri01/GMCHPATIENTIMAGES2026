using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : BaseController
    {
        private IRegistrationService _registrationService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        public RegistrationController(IRegistrationService registrationService, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _registrationService = registrationService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;    
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RegistrationRequestDTO registrationRequestDTO)
        {
            registrationRequestDTO.UserIdC = currentUser.LoginId;
            registrationRequestDTO.Mode = "view";
            var response = await _registrationService.GetAllAsync(registrationRequestDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            registrationRequestDTO.UserIdC = currentUser.LoginId;
            registrationRequestDTO.Mode = "insert";
            var response = await _registrationService.InsertAsync(registrationRequestDTO);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            registrationRequestDTO.UserIdC = currentUser.LoginId;
            registrationRequestDTO.Mode = "update";
            var response = await _registrationService.UpdateAsync(registrationRequestDTO);
            return Ok(response);
        }
    }    
}
