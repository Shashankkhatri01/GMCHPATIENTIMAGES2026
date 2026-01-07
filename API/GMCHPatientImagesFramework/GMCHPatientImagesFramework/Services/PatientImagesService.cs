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
    public class PatientImagesService : IPatientImagesService
  {
        private readonly AppSettings _appSettings;
        private IPatientImagesRepository _repository;

        public PatientImagesService(IPatientImagesRepository repository, IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;
            _repository = repository;
        }

    //Insert
    public async Task<ReturnObject<long>> Insert(PatientImagesDTO patientImagesDTO)
    {

          if (patientImagesDTO.PatientNo == "")
            throw new AppException($"Please enter Patient No. !");
          //if (patientImagesDTO.ImageName == null)
          //  throw new AppException($"Please enter Image Name!");
          if (patientImagesDTO.DoctorName == null)
            throw new AppException($"Please enter Doctor Name!");
          //if (patientImagesDTO.DepartmentName == null)
          //  throw new AppException($"Please enter Department Name!");
            var response = await _repository.InsertAsync(patientImagesDTO);

      if (response == 0)
        throw new AppException($"Image {StringConstants.SavedFailed}");
        
      return new ReturnObject<long>
      {
        Message = $"Image {StringConstants.SavedSuccess}",
        ReturnValue = response,
        Status = true,
        Success = true
      };
    }
    //Update
    public async Task<ReturnObject<long>> Delete(PatientImagesDTO patientImagesDTO)
    { 

      var response = await _repository.UpdateAsync(patientImagesDTO);

      if (response == 0)
        throw new AppException($"Image {StringConstants.DeletionFailed}");
        
      return new ReturnObject<long>
      {
        Message = $"Image {StringConstants.DeleteSuccess}",
        ReturnValue = response,
        Status = true,
        Success = true
      };
    }

    public async Task<ReturnObject<List<PatientImagesDTO>>> GetAll(PatientImagesDTO patientImagesDTO)
    {
      var response = await _repository.GetAllAsync(patientImagesDTO);

      if (response.Count <= 0)
        throw new AppException($"User {StringConstants.RecordNotFound}");

      return new ReturnObject<List<PatientImagesDTO>>
      {
        ReturnValue = response,
        Status = true,
        Success = true
      };
    }



  }
}
