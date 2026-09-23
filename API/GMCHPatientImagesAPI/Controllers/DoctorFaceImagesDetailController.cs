using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/DoctorFaceImagesDetail")]
    [ApiController]
    public class DoctorFaceImagesDetailController : BaseController
    {
        private IDoctorFaceImagesDetailService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public DoctorFaceImagesDetailController(IDoctorFaceImagesDetailService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO)
        {
            doctorFaceImagesDetailDTO.UserIdC = currentUser.LoginId;
            doctorFaceImagesDetailDTO.Mode = "search";
            var response = await _service.GetAllAsync(doctorFaceImagesDetailDTO);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DoctorFaceImagesDetailDTO doctorFaceImagesDetailDTO)
        {
            doctorFaceImagesDetailDTO.UserIdC = currentUser.LoginId;
            doctorFaceImagesDetailDTO.Mode = "delete";
            var response = await _service.DeleteAsync(doctorFaceImagesDetailDTO);
            return Ok(response);
        }
    }
}
