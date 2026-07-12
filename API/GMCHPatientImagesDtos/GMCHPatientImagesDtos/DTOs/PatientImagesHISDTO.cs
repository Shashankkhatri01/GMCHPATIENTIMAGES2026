using GMCHPatientImagesDtos.DTOs;
using System;

namespace ConfigurationDtos.DTOs
{
    public class PatientImagesHISDTO : BaseDTO
    {
        public string HIS_ID { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public int PatientTypeId { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }        
        public string MobileNo { get; set; }
        public string PayerName { get; set; }
        public string WardName { get; set; }
        public string BedNumber { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; } //Specialisation 
        public int CaseTypeId { get; set; }
        public string HISStatus { get; set; }
        public string UniqueID { get; set; }
    }
}
