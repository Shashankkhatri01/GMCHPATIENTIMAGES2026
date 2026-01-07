using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GMCHPatientImagesDtos.Attributes;

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
  }
}
