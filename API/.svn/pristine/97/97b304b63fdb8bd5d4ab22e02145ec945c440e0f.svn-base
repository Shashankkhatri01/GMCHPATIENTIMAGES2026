using GMCHPatientImagesDtos.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImages.Controllers
{
    public class BaseController : ControllerBase
    {
     public LoginDTO currentUser => HttpContext.Items["CurrentUser"] != null ? (LoginDTO)HttpContext.Items["CurrentUser"] : null;
    //public LoginDTO currentUser => new LoginDTO
    //{
    //  LoginId = 1,
    //  //ClientId = 1

    //};

  }
}
