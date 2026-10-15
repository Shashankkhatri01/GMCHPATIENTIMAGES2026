using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/PatientAdmissionImagesDetail")]
    [ApiController]
    public class PatientAdmissionImagesDetailController : BaseController
    {
        private IPatientAdmissionImagesDetailService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public PatientAdmissionImagesDetailController(IPatientAdmissionImagesDetailService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PatientAdmissionImagesDetailDTO patientAdmissionImagesDetailDTO)
        {
            patientAdmissionImagesDetailDTO.UserIdC = currentUser.LoginId;
            patientAdmissionImagesDetailDTO.Mode = "search";
            var response = await _service.GetAllAsync(patientAdmissionImagesDetailDTO);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] PatientAdmissionImagesDetailDTO patientAdmissionImagesDetailDTO)
        {
            patientAdmissionImagesDetailDTO.UserIdC = currentUser.LoginId;
            patientAdmissionImagesDetailDTO.Mode = "delete";
            var response = await _service.DeleteAsync(patientAdmissionImagesDetailDTO);
            return Ok(response);
        }
    }
}
