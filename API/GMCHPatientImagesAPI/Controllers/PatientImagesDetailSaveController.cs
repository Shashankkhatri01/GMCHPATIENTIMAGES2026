using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/PatientImagesDetailSave")]
    [ApiController]
    public class PatientImagesDetailSaveController : BaseController
    {
        private IPatientImagesDetailSaveService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public PatientImagesDetailSaveController(IPatientImagesDetailSaveService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PatientImagesDetailSaveDTO patientImagesDetailSaveDTO)
        {
            patientImagesDetailSaveDTO.UserIdC = currentUser.LoginId;
            patientImagesDetailSaveDTO.Mode = "insert";
            var response = await _service.InsertAsync(patientImagesDetailSaveDTO);
            return Ok(response);
        }
    }
}
