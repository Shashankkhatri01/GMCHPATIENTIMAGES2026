using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class HealthCheckResponseService : IHealthCheckResponseService
    {
        private readonly AppSettings _appSettings;

        public HealthCheckResponseService(IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
        }

        public async Task<ReturnObject<HealthCheckResponseDTO>> GetHealthAsync()
        {
            var response = new HealthCheckResponseDTO
            {
                Status = "Healthy",
                Message = "API is running successfully.",
                ServerTimeUTC = DateTime.UtcNow,
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            };

            return await Task.FromResult(new ReturnObject<HealthCheckResponseDTO>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            });
        }
    }
}
