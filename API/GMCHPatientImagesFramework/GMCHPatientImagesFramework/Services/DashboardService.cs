using ConfigurationDtos.DTOs;
using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppSettings _appSettings;
        private IDashboardRepository _repository;
        private readonly IWhatsAppAPIService _whatsAppService;

        public DashboardService(IDashboardRepository repository, IWhatsAppAPIService whatsAppAPIService, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
            _whatsAppService = whatsAppAPIService;
        }

        public async Task<ReturnObject<DashboardResponseDTO>> GetAllAsync(
    DashboardRequestDTO dashboardRequestDTO)
        {
            try
            {
                var response = await _repository
                    .GetMultiResultAsync<DashboardResponseDTO>(dashboardRequestDTO);

                if (response == null || response.Counters == null)
                    throw new AppException($"Records {StringConstants.RecordNotFound}");

                // Club DoctorStatus into DoctorDepartment
                if (response.DoctorDepartment != null)
                {
                    foreach (var doctor in response.DoctorDepartment)
                    {
                        doctor.Status = response.DoctorStatus?
                            .Where(x =>
                                x.DoctorId == doctor.DoctorId &&
                                x.DepartmentId == doctor.DepartmentId
                            )
                            .ToList()
                            ?? new List<DashboardDoctorStatusListDTO>();
                    }
                }

                response.DoctorStatus = null;

                if (response.Department != null)
                {
                    foreach (var department in response.Department)
                    {
                        department.Status = response.DepartmentStatus?
                            .Where(x =>
                                x.DepartmentId == department.DepartmentId
                            )
                            .ToList()
                            ?? new List<DashboardDepartmentStatusListDTO>();
                    }
                }

                response.DepartmentStatus = null;

                return new ReturnObject<DashboardResponseDTO>
                {
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);

                return new ReturnObject<DashboardResponseDTO>
                {
                    ReturnValue = null,
                    Message = "Details",
                    Status = false,
                    Success = false
                };
            }
        }
    }
}
