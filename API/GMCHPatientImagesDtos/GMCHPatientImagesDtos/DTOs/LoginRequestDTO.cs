using GMCHPatientImagesDtos.Attributes;
using System.Text.Json.Serialization;

namespace GMCHPatientImagesDtos.DTOs
{
    public class LoginRequestDTO
    {
       [JsonIgnore]
       public long LoginId { get; set; }
       public string LoginName { get; set; }
       public string LoginPassword { get; set; }
       [JsonIgnore]
       public string Mode { get; set; }
       [IgnoreParam]
       public int TokenVersion { get; set; }
       [IgnoreParam]
       public bool IsActive { get; set; }
    }
}
