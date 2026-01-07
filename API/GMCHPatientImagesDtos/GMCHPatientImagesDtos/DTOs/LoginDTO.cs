using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GMCHPatientImagesDtos.Attributes;

namespace GMCHPatientImagesDtos.DTOs
{
    public class LoginDTO 
    {
        public long LoginId { get; set; }  
        public string LoginName { get; set; }  
         public string MobileNo { get; set; } 
        public string EmailAddress { get; set; }
        public Nullable<bool> IsView { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsAdd { get; set; } 
        [JsonIgnore]
        public string Mode { get; set; }
        [JsonIgnore]
        public Nullable<int> UserIdC { get; set; } 
        [IgnoreParam]
        public string Token { get; set; } 
        [IgnoreParam]
        public string RefreshToken { get; set; } 

  }
}
