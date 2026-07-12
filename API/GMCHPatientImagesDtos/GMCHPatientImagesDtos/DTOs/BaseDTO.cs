 using System; 
using System.Text.Json.Serialization;
 
namespace GMCHPatientImagesDtos.DTOs
{
    public class BaseDTO
    {
        public int TotalRecords { get; set; }
        public Nullable<bool> IsActive { get; set; }
        [JsonIgnore]
        public Nullable<long> UserIdC { get; set; }
        public string Mode { get; set; } 
  }
}
