using ConfigurationDtos.DTOs;
using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Services.Interfaces 
{
    public interface INotificationService
    {
        Task<ReturnObject<List<NotificationDTO>>> GetAllAsync(NotificationDTO notificationDTO);
        Task<ReturnObject<long>> UpdateAsync(NotificationDTO notificationDTO);
    }
}
