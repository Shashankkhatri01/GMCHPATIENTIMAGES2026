using GMCHPatientImages.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;
        private AppSettings _appSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
         

        public UserController(IUserService userService, IWebHostEnvironment hostEnvironment, IConfiguration configuration ,IOptions<AppSettings> settings)
        {
            _userService = userService;
            _webHostEnvironment = hostEnvironment;
            _configuration = configuration;          
            _appSettings = settings.Value;

        } 
    
        //Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginDTO>> Login(LoginRequestDTO userDto)
        { 
            userDto.Mode = "login";
            var response = await _userService.LoginAsync(userDto,"");
            return Ok(response);
        }

        //Token
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginDTO>> RefreshToken([FromQuery] string refreshToken)
        {
            var response = await _userService.RefreshTokenAsync(refreshToken,"");
           
            return Ok(response);
        }

    //Password Change
    [Authorize]
    [HttpPost("change-password")]
        public async Task<ActionResult<long>> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
             changePasswordDTO.LoginId = currentUser.LoginId;
            changePasswordDTO.Mode = "changepassword";
            var response = await _userService.ChangePasswordAsync(changePasswordDTO);

            return Ok(response);
        }

        //ForgotPassword
        [HttpPut]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginDTO loginDTO)
        {
            loginDTO.Mode = "forgot-password";

            var response = await _userService.ForgotPassword(loginDTO);

      return Ok(response.Success);
        }

        

       

          
    }    
}
