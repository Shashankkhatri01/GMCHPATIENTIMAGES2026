using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class PatientImagesDetailService : IPatientImagesDetailService
    {
        private readonly AppSettings _appSettings;
        private IPatientImagesDetailRepository _repository;

        public PatientImagesDetailService(IPatientImagesDetailRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }
        public async Task<ReturnObject<List<PatientImagesDetailResponseDTO>>> GetAllAsync(PatientImagesDetailDTO patientImagesDetailDTO)
        {
            var response = await _repository.GetAllAsync(patientImagesDetailDTO);

            if (response == null || !response.Any())
                throw new AppException($"Records {StringConstants.RecordNotFound}");

            var result = response
                .GroupBy(x => new
                {
                    x.StatusName
                })
                .Select(g =>
                {
                    var first = g.First();

                    return new PatientImagesDetailResponseDTO
                    {
                        PatientImagesId = first.PatientImagesId,
                        StatusName = first.StatusName,

                        particularsDTOs = g
                            .Select(x => new PatientImagesParticularsDTO
                            {
                                PatientImagesDetailId = x.PatientImagesDetailId,
                                UserName = x.UserName,
                                Latitute = x.Latitute,
                                Longitute = x.Longitute,
                                LocationName = x.LocationName,
                                ImageName = x.ImageName,
                                ImageFull = x.ImageFull,
                                CrDate = x.CrDate
                            })
                            .OrderBy(x => x.CrDate)
                            .ToList()
                    };
                })
                .ToList();

            return new ReturnObject<List<PatientImagesDetailResponseDTO>>
            {
                ReturnValue = result,
                Status = true,
                Success = true
            };
        }
        public async Task<ReturnObject<long>> DeleteAsync(PatientImagesDetailDTO patientImagesDetailDTO)
        {
            var response = await _repository.DeleteAsync(patientImagesDetailDTO);

            if (response <=0)
                throw new AppException($"Image {StringConstants.DeletionFailed}");

            else if (response == -2)
                return new ReturnObject<long>
                {
                    Message = $"Patient is Locked",
                    ReturnValue = response,
                    Status = true,
                    Success = false,
                };

            return new ReturnObject<long>
            {
                Message = $"Image {StringConstants.DeleteSuccess}",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}
