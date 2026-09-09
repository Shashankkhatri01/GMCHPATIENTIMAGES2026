using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/consent")]
    [ApiController]
    public class ConsentController : BaseController
    {
        private IConsentService _consentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        public ConsentController(IConsentService consentService, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _consentService = consentService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;    
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ConsentRequestDTO consentRequestDTO)
        {
            consentRequestDTO.UserIdC = currentUser.LoginId;
            consentRequestDTO.Mode = "view";
            var response = await _consentService.GetAllAsync(consentRequestDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConsentRequestDTO consentRequestDTO)
        {
            consentRequestDTO.UserIdC = currentUser.LoginId;
            consentRequestDTO.Mode = "insert";
            var response = await _consentService.InsertAsync(consentRequestDTO);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ConsentRequestDTO consentRequestDTO)
        {
            consentRequestDTO.UserIdC = currentUser.LoginId;
            consentRequestDTO.Mode = "update";
            var response = await _consentService.UpdateAsync(consentRequestDTO);
            return Ok(response);
        }
    }    
}
