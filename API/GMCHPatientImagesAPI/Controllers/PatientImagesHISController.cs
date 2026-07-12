using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/txnpatientimages")]
    [ApiController]
    public class PatientImagesHISController : BaseController
    {
        private IPatientImagesHISService _patientImagesHISService;
        public PatientImagesHISController(IPatientImagesHISService patientImagesHISService)
        {
            _patientImagesHISService = patientImagesHISService;
        }

        [HttpPost]
        [ApiKeyAuth]
        public async Task<IActionResult> Post([FromBody] PatientImagesHISDTO babyVaccinationRequestHISDTO)
        {
            babyVaccinationRequestHISDTO.Mode = "insert";
            var response = await _patientImagesHISService.InsertAsync(babyVaccinationRequestHISDTO);

            if (!response.Success)
            {
                // Example: if SaveFailed (-1) or AlreadyExists (-2)
                if (response.ReturnValue == -2)
                    return Conflict(response); // HTTP 409 for duplicate

                if (response.ReturnValue == 0 || response.ReturnValue == -1)
                    return StatusCode(StatusCodes.Status500InternalServerError, response); // HTTP 500 for failure

                return BadRequest(response); // fallback
            }
            return Ok(response);
        }

        [HttpPut]
        [ApiKeyAuth]
        public async Task<IActionResult> Put([FromBody] PatientImagesHISDTO babyVaccinationRequestHISDTO)
        {
            babyVaccinationRequestHISDTO.Mode = "update";
            var response = await _patientImagesHISService.UpdateAsync(babyVaccinationRequestHISDTO);

            if (!response.Success)
            {
                // Example: if SaveFailed (-1) or AlreadyExists (-2)
                if (response.ReturnValue == -2)
                    return Conflict(response); // HTTP 409 for duplicate

                if (response.ReturnValue == 0 || response.ReturnValue == -1)
                    return StatusCode(StatusCodes.Status500InternalServerError, response); // HTTP 500 for failure

                return BadRequest(response); // fallback
            }
            return Ok(response);
        }

        [HttpPut]
        [ApiKeyAuth]
        [Route("updatestatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] PatientImagesHISDTO babyVaccinationRequestHISDTO)
        {
            babyVaccinationRequestHISDTO.Mode = "updatestatus";
            var response = await _patientImagesHISService.UpdateAsync(babyVaccinationRequestHISDTO);

            if (!response.Success)
            {
                // Example: if SaveFailed (-1) or AlreadyExists (-2)
                if (response.ReturnValue == -2)
                    return Conflict(response); // HTTP 409 for duplicate

                if (response.ReturnValue == 0 || response.ReturnValue == -1)
                    return StatusCode(StatusCodes.Status500InternalServerError, response); // HTTP 500 for failure

                return BadRequest(response); // fallback
            }
            return Ok(response);
        }
    }    
}
