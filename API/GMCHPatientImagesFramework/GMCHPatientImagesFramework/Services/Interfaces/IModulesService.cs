using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces
{
    public interface IModulesService
    {
        //Get All Data
        Task<ReturnObject<List<ModulesDTO>>> GetAllAsync(ModulesDTO modulesRequestDTO);
    }
}
