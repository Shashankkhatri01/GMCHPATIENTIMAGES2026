using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/DoctorFaceImagesSave")]
    [ApiController]
    public class DoctorImagesSaveController : BaseController
    {
        private IDoctorImagesSaveService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public DoctorImagesSaveController(IDoctorImagesSaveService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] DoctorFaceImagesSaveDTO doctorFaceImagesSaveDTO)
        {
            doctorFaceImagesSaveDTO.UserIdC = currentUser.LoginId;
            doctorFaceImagesSaveDTO.Mode = "insert";
            var response = await _service.InsertAsync(doctorFaceImagesSaveDTO);
            return Ok(response);
        }
    }
}
