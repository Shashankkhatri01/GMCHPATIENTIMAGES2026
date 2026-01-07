using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/dropdowns")]
    [ApiController]
    public class DropdownsController : BaseController
    {
        private IDropdownsService _Service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public DropdownsController(IDropdownsService Service, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _Service = Service;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        //Get All Dropdowns
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DropdownsDTO dropdownsDTO)
            {
            dropdownsDTO.UserIdC = currentUser.LoginId; 
            var response = await _Service.GetAll(dropdownsDTO);
            return Ok(response);
        }
    }
}
