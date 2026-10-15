using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/PatientAdmissionImagesSave")]
    [ApiController]
    public class PatientAdmissionImagesSaveController : BaseController
    {
        private IPatientAdmissionImagesSaveService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public PatientAdmissionImagesSaveController(IPatientAdmissionImagesSaveService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PatientAdmissionImagesSaveDTO patientAdmissionImagesSaveDTO)
        {
            patientAdmissionImagesSaveDTO.UserIdC = currentUser.LoginId;
            patientAdmissionImagesSaveDTO.Mode = "insert";
            var response = await _service.InsertAsync(patientAdmissionImagesSaveDTO);
            return Ok(response);
        }
    }
}
