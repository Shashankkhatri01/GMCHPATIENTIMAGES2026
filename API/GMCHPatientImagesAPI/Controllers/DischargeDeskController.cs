using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/DischargeDesk")]
    [ApiController]
    public class DischargeDeskController : BaseController
    {
        private IDischargeDeskService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public DischargeDeskController(IDischargeDeskService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        //Get All
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] DischargeDeskRequestDTO dischargeDeskRequestDTO)
        {
            dischargeDeskRequestDTO.UserIdC = currentUser.LoginId;
            dischargeDeskRequestDTO.Mode = "search";
            var response = await _service.GetAllAsync(dischargeDeskRequestDTO);
            return Ok(response);
        }
    }
}
