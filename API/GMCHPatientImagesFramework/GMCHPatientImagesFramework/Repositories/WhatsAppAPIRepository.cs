using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GMCHPatientImagesFramework.Repositories
{
    public class WhatsAppAPIRepository : RepositoryBase<WhatsAppLogRequestDTO, WhatsAppLogRequestDTO>, IWhatsAppAPIRepository
    {
        //Procedure Route
        public WhatsAppAPIRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "WhatsAppLogMaster_crud";
        }
    } 
}
