using GMCHPatientImagesDtos.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GMCHPatientImages.Controllers
{
    public class BaseController : ControllerBase
    {
        //public LoginDTO currentUser => HttpContext.Items["CurrentUser"] != null ? (LoginDTO)HttpContext.Items["CurrentUser"] : null;
        //public LoginDTO currentUser => new LoginDTO
        //{
        //  LoginId = 1,
        //  //ClientId = 1

        //};
        public LoginDTO currentUser
        {
            get
            {
                var user = HttpContext.Items["CurrentUser"] as LoginDTO;
                if (user == null)
                {
                    throw new UnauthorizedAccessException("User is not authorized.");
                }
                return user;
            }
        }
    }
}
