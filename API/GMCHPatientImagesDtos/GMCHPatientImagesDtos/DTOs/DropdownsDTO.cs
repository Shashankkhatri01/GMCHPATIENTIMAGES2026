using GMCHPatientImagesDtos.Attributes;
using System;
using System.Text.Json.Serialization;

namespace GMCHPatientImagesDtos.DTOs
{
    public class DropdownsDTO : BaseDTO
    {
        [IgnoreParam]
        public long Id { get; set; }
        [IgnoreParam]
        public string Value { get; set; }
        [JsonIgnore]
        public int Cond1 { get; set; } 
        [JsonIgnore]
        public int Cond2 { get; set; }
        [JsonIgnore]
        public int Cond3 { get; set; } 
    }
}
