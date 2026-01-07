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
    public class UserMenuRepository : RepositoryBase<UserMenuDTO, UserMenuDTO>, IUserMenuRepository
    {
        public UserMenuRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "MenuMasterList";
        }

    }
}
