using ConfigurationDtos.DTOs;
using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class DischargeDeskService : IDischargeDeskService
    {
        private readonly AppSettings _appSettings;
        private IDischargeDeskRepository _repository;

        public DischargeDeskService(IDischargeDeskRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }

        public async Task<ReturnObject<DischargeDeskResponseDTO>> GetAllAsync(DischargeDeskRequestDTO dischargeDeskRequestDTO)
        {
            try
            {
                var response = await _repository.GetAllAsync(dischargeDeskRequestDTO);

                if (response == null || !response.Any())
                    throw new AppException($"Records {StringConstants.RecordNotFound}");

                var first = response.First();

                var result = new DischargeDeskResponseDTO
                {
                    Detail = new DischargeDeskDetailDTO
                    {
                        PatientImagesId = first.PatientImagesId,
                        HIS_ID = first.HIS_ID,
                        AdmissionNo = first.AdmissionNo,
                        AdmissionDate = first.AdmissionDate,
                        PatientTypeName = first.PatientTypeName,
                        PatientName = first.PatientName,
                        DOB = first.DOB,
                        Gender = first.Gender,
                        MobileNo = first.MobileNo,
                        PayerName = first.PayerName,
                        WardName = first.WardName,
                        BedNumber = first.BedNumber,
                        DoctorName = first.DoctorName,
                        DepartmentName = first.DepartmentName,
                        CaseTypeName = first.CaseTypeName,
                        IsOutside = first.IsOutside,
                        HISStatus = first.HISStatus,
                        CurrentStatus = first.CurrentStatus,
                        Remark = first.Remark,
                        CrDate = first.CrDate,
                        UserName = first.UserName,
                        IsLock = first.IsLock
                    },

                    Images = response
                        .Where(x => !string.IsNullOrWhiteSpace(x.ImageName))
                        .Select(x => new DischargeDeskImagesDTO
                        {
                            ImageName = x.ImageName,
                            ImageFull = x.ImageFull,
                            Latitute = x.Latitute,
                            Longitute = x.Longitute,
                            LocationName = x.LocationName,
                            StatusName = x.StatusName
                        })
                        .ToList()
                };

                return new ReturnObject<DischargeDeskResponseDTO>
                {
                    ReturnValue = result,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);
                return new ReturnObject<DischargeDeskResponseDTO>
                {
                    Message = $"Details",
                    ReturnValue = null,
                    Status = false,
                    Success = false
                };
            }
        }
    }
}
