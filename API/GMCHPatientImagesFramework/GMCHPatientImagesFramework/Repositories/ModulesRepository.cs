using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class ModulesRepository : RepositoryBase<ModulesDTO, ModulesDTO>, IModulesRepository
    {
        //Procedure Route
        public ModulesRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "Modules_cr";
        }
    } 
}
