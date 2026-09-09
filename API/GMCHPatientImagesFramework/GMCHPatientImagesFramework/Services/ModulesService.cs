using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class ModulesService : IModulesService
    {
        private readonly AppSettings _appSettings;
        private IModulesRepository _modulesRepository;

        public ModulesService(IModulesRepository modulesRepository, IOptions<AppSettings> appSettings)
        {
            _modulesRepository = modulesRepository;
            _appSettings = appSettings.Value;
        }

        //Get All data
        public async Task<ReturnObject<List<ModulesDTO>>> GetAllAsync(ModulesDTO modulesRequestDTO)
        {
            var response = await _modulesRepository.GetAllAsync(modulesRequestDTO);

            if (response == null || response.Count <=0)
                throw new AppException($"Modules {StringConstants.RecordNotFound}");

            return new ReturnObject<List<ModulesDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}

