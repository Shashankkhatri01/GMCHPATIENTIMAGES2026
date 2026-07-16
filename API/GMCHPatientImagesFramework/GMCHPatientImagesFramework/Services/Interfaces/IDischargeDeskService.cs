using ConfigurationDtos.DTOs;
using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces 
{
    public interface IDischargeDeskService
    {
        Task<ReturnObject<DischargeDeskResponseDTO>> GetAllAsync(DischargeDeskRequestDTO dischargeDeskRequestDTO);
        Task<ReturnObject<long>> UpdateAsync(DischargeDeskRequestDTO dischargeDeskRequestDTO);
    }
}
