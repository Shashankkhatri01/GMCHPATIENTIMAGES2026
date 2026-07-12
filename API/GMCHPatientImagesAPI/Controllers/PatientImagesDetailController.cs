using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/PatientImagesDetail")]
    [ApiController]
    public class PatientImagesDetailController : BaseController
    {
        private IPatientImagesDetailService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public PatientImagesDetailController(IPatientImagesDetailService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PatientImagesDetailDTO patientImagesDetailDTO)
        {
            patientImagesDetailDTO.UserIdC = currentUser.LoginId;
            patientImagesDetailDTO.Mode = "search";
            var response = await _service.GetAllAsync(patientImagesDetailDTO);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _service.DeleteAsync(id);
            return Ok(response);
        }
    }
}
