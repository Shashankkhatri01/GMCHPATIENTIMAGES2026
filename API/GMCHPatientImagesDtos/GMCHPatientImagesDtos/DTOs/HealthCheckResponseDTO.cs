 using System; 
using System.Text.Json.Serialization;
 
namespace GMCHPatientImagesDtos.DTOs
{
    public class HealthCheckResponseDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime ServerTimeUTC { get; set; }
        public string Version { get; set; }
    }
}
