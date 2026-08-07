using ConfigurationDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class NotificationRepository : RepositoryBase<NotificationDTO, NotificationDTO>, INotificationRepository
  {
        public NotificationRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "Notification_crud";
        }

    }
}
