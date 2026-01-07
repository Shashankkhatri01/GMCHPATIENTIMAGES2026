using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Repositories
{
    public class LoginRepository : RepositoryBase<LoginRequestDTO, LoginRequestDTO>, ILoginRepository
  {
        //Procedure Route
        public LoginRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "Login_curd";
        }
    }
}
