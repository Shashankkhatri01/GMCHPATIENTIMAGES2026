using GMCHPatientImages.Utils;
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
    [Route("api/UserMenu")]
    [ApiController]
    public class UserMenuController : BaseController
    {
        private IUserMenuService _UserMenuService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;

        public UserMenuController(IUserMenuService UserMenuService, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _UserMenuService = UserMenuService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("getusermenu")]

        public async Task<IActionResult> GetMenu([FromQuery] UserMenuDTO userMenuDTO)
        {
            
            userMenuDTO.Mode = "getmenus";
            userMenuDTO.UserId = currentUser.LoginId;
            var response = await _UserMenuService.GetAll(userMenuDTO);
            return Ok(response);
        }


    }
}
