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

        public async Task<ReturnObject<List<PatientImagesDTO>>> GetAllAsync(PatientImagesDTO patientImagesDTO)
        {
            var response = await _repository.GetAllAsync(patientImagesDTO);

            if (response.Count <= 0)
                throw new AppException($"Records {StringConstants.RecordNotFound}");

            return new ReturnObject<List<PatientImagesDTO>>
            {
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
        public async Task<ReturnObject<long>> InsertAsync(PatientImagesDTO patientImagesDTO)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(patientImagesDTO.HIS_ID))
                throw new AppException("Please enter HIS ID!");

                if (string.IsNullOrWhiteSpace(patientImagesDTO.AdmissionNo))
                    throw new AppException("Please enter Admission No!");

                if (!patientImagesDTO.AdmissionDate.HasValue)
                    throw new AppException("Please select Admission Date!");

                if (string.IsNullOrWhiteSpace(patientImagesDTO.PatientName))
                    throw new AppException("Please enter Patient Name!");

                if (!patientImagesDTO.DOB.HasValue)
                    throw new AppException("Please select Date of Birth!");

                if (string.IsNullOrWhiteSpace(patientImagesDTO.Gender))
                    throw new AppException("Please select Gender!");

                if (string.IsNullOrWhiteSpace(patientImagesDTO.MobileNo))
                    throw new AppException("Please enter Mobile No!");

                if (string.IsNullOrWhiteSpace(patientImagesDTO.PayerName))
                    throw new AppException("Please enter Payer Name!");

                if (string.IsNullOrWhiteSpace(patientImagesDTO.DoctorName))
                    throw new AppException("Please enter Doctor Name!");

                if (patientImagesDTO.DepartmentId <= 0)
                    throw new AppException("Please select Department Name!");

                var response = await _repository.InsertAsync(patientImagesDTO);

                if (response == 0)
                    throw new AppException($"Record {StringConstants.SavedFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"HIS ID {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Record {StringConstants.SavedSuccess}",
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);
                return new ReturnObject<long>
                {
                    Message = $"Details Exception",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
        public async Task<ReturnObject<long>> UpdateAsync(PatientImagesDTO patientImagesDTO)
        {
            try
            {
                if (patientImagesDTO.PatientImagesId <= 0)
                    throw new AppException("Update ID not passed");

                if (patientImagesDTO.Mode.Equals("search"))
                {
                    if (!patientImagesDTO.AdmissionDate.HasValue)
                        throw new AppException("Please select Admission Date!");

                    if (string.IsNullOrWhiteSpace(patientImagesDTO.PatientName))
                        throw new AppException("Please enter Patient Name!");

                    if (!patientImagesDTO.DOB.HasValue)
                        throw new AppException("Please select Date of Birth!");

                    if (string.IsNullOrWhiteSpace(patientImagesDTO.Gender))
                        throw new AppException("Please select Gender!");

                    if (string.IsNullOrWhiteSpace(patientImagesDTO.MobileNo))
                        throw new AppException("Please enter Mobile No!");

                    if (string.IsNullOrWhiteSpace(patientImagesDTO.PayerName))
                        throw new AppException("Please enter Payer Name!");

                    if (string.IsNullOrWhiteSpace(patientImagesDTO.DoctorName))
                        throw new AppException("Please enter Doctor Name!");

                    if (patientImagesDTO.DepartmentId <= 0)
                        throw new AppException("Please select Department Name!");
                }

                var response = await _repository.UpdateAsync(patientImagesDTO);

                if (response == 0)
                    throw new AppException($"Record {StringConstants.UpdateFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient is Locked",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -3)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient is not discharged from HIS.",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -4)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient Images Exist! You cannot change case type",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Record {StringConstants.UpdateSuccess}",
                    ReturnValue = response,
                    Status = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Helper.WriteMsg(ex);
                return new ReturnObject<long>
                {
                    Message = $"Details Exception",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
        public async Task<ReturnObject<bool>> DeleteAsync(long id)
        { 
            var response = await _repository.DeleteAsync(id);

            if (!response)
                throw new AppException($"Record {StringConstants.DeletionFailed}");

            return new ReturnObject<bool>
            {
                Message = $"Record {StringConstants.DeleteSuccess}",
                ReturnValue = response,
                Status = true,
                Success = true
            };
        }
    }
}
