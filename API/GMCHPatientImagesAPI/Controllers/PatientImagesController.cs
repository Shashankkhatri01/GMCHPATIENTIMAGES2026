using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/PatientImages")]
    [ApiController]
    public class PatientImagesController : BaseController
    {
        private IPatientImagesService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public PatientImagesController(IPatientImagesService service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _service = service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        //Get All
        [HttpPost]
        [Route("getall")]
        public async Task<IActionResult> Get([FromBody] PatientImagesDTO patientImagesDTO)
        {
            patientImagesDTO.UserIdC = currentUser.LoginId;
            patientImagesDTO.Mode = patientImagesDTO.Mode ?? "search"; //searchautosuggest
            var response = await _service.GetAllAsync(patientImagesDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PatientImagesDTO patientImagesDTO)
        {
            patientImagesDTO.UserIdC = currentUser.LoginId;
            patientImagesDTO.Mode = "insert";
            var response = await _service.InsertAsync(patientImagesDTO);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PatientImagesDTO patientImagesDTO)
        {
            patientImagesDTO.UserIdC = currentUser.LoginId;
            patientImagesDTO.Mode = patientImagesDTO.Mode ?? "update"; //update status, update case type
            var response = await _service.UpdateAsync(patientImagesDTO);
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
