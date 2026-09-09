using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : BaseController
    {
        private IModulesService _modulesService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        public ModulesController(IModulesService modulesService, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _modulesService = modulesService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;    
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ModulesDTO modulesDTO)
        {
            modulesDTO.Mode = "view";
            var response = await _modulesService.GetAllAsync(modulesDTO);
            return Ok(response);
        }
    }    
}
