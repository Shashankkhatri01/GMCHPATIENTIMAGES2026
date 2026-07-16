using ConfigurationDtos.DTOs;
using GMCHPatientImages.Framework.Utils;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using GMCHPatientImagesFramework.Services.Interfaces;
using GMCHPatientImagesFramework.Type;
using GMCHPatientImagesFramework.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services
{
    public class PatientImagesHISService : IPatientImagesHISService
    {
        private readonly AppSettings _appSettings;
        private IPatientImagesHISRepository _patientImagesHISRepository;  

        public PatientImagesHISService(IPatientImagesHISRepository patientImagesHISRepository, IOptions<AppSettings> appSettings)
        {
            _patientImagesHISRepository = patientImagesHISRepository;           
            _appSettings = appSettings.Value;
        }

        public async Task<ReturnObject<long>> InsertAsync(PatientImagesHISDTO patientImagesHISDTO)
        {
            try
            {
                if(String.IsNullOrEmpty(patientImagesHISDTO.UniqueID))
                {
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.RecordNotFound}",
                        ReturnValue = -4,
                        Status = true,
                        Success = false
                    };
                }

                var response = await _patientImagesHISRepository.InsertAsync(patientImagesHISDTO);

                if (response == 0)
                    throw new AppException($"Details {StringConstants.SavedFailed}");

                else if (response == -2)
                    return new ReturnObject<long>
                    {
                        Message = $"HIS ID {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                //else if (response == -3)
                //    return new ReturnObject<long>
                //    {
                //        Message = $"Baby Name, Father Name and Mobile No {StringConstants.AlreadyExists}",
                //        ReturnValue = response,
                //        Status = true,
                //        Success = false,
                //    };

                else if (response == -4)
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.RecordNotFound}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -5)
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Details {StringConstants.SavedSuccess}",
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
                    Message = $"Details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
        public async Task<ReturnObject<long>> UpdateAsync(PatientImagesHISDTO patientImagesHISDTO)
        {
            try
            {
                if (String.IsNullOrEmpty(patientImagesHISDTO.UniqueID))
                {
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.RecordNotFound}",
                        ReturnValue = -4,
                        Status = true,
                        Success = false
                    };
                }

                var response = await _patientImagesHISRepository.UpdateAsync(patientImagesHISDTO);

                if (response == 0)
                    throw new AppException($"Details {StringConstants.UpdateFailed}");

                //else if (response == -2)
                //    return new ReturnObject<long>
                //    {
                //        Message = $"Details {StringConstants.AlreadyExists}",
                //        ReturnValue = response,
                //        Status = true,
                //        Success = false,
                //    };

                //else if (response == -3)
                //    return new ReturnObject<long>
                //    {
                //        Message = $"Baby Name, Father Name and Mobile No {StringConstants.AlreadyExists}",
                //        ReturnValue = response,
                //        Status = true,
                //        Success = false,
                //    };

                else if (response == -4)
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.RecordNotFound}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -5)
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -6)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient is Locked",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Details {StringConstants.UpdateSuccess}",
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
                    Message = $"Details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }

        public async Task<ReturnObject<long>> UpdateStatusAsync(PatientImagesHISDTO patientImagesHISDTO)
        {
            try
            {
                if (String.IsNullOrEmpty(patientImagesHISDTO.UniqueID))
                {
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.RecordNotFound}",
                        ReturnValue = -4,
                        Status = true,
                        Success = false
                    };
                }

                var response = await _patientImagesHISRepository.UpdateAsync(patientImagesHISDTO);

                if (response == 0)
                    throw new AppException($"Details {StringConstants.UpdateFailed}");

                //else if (response == -2)
                //    return new ReturnObject<long>
                //    {
                //        Message = $"Details {StringConstants.AlreadyExists}",
                //        ReturnValue = response,
                //        Status = true,
                //        Success = false,
                //    };

                //else if (response == -3)
                //    return new ReturnObject<long>
                //    {
                //        Message = $"Baby Name, Father Name and Mobile No {StringConstants.AlreadyExists}",
                //        ReturnValue = response,
                //        Status = true,
                //        Success = false,
                //    };

                else if (response == -4)
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.RecordNotFound}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -5)
                    return new ReturnObject<long>
                    {
                        Message = $"Unique ID {StringConstants.AlreadyExists}",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                else if (response == -6)
                    return new ReturnObject<long>
                    {
                        Message = $"Patient is Locked",
                        ReturnValue = response,
                        Status = true,
                        Success = false,
                    };

                return new ReturnObject<long>
                {
                    Message = $"Details {StringConstants.UpdateSuccess}",
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
                    Message = $"Details",
                    ReturnValue = -1,
                    Status = false,
                    Success = false
                };
            }
        }
    }
}

