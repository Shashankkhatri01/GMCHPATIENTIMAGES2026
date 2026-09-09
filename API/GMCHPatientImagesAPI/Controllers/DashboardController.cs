using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private IDashboardService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public DashboardController(IDashboardService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        //Get All
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] DashboardRequestDTO dashboardRequestDTO)
        {
            dashboardRequestDTO.UserIdC = currentUser.LoginId;
            dashboardRequestDTO.Mode = "search";
            var response = await _service.GetAllAsync(dashboardRequestDTO);
            return Ok(response);
        }
    }
}
