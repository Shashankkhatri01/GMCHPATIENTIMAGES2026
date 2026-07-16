using GMCHPatientImagesDtos.Attributes;
using GMCHPatientImagesDtos.DTOs;
using System;

namespace ConfigurationDtos.DTOs
{
    public class PatientImagesDTO : BaseDTO
    {
        public long ID { get; set; }
        public long PatientImagesId { get; set; }
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
        public string DepartmentName { get; set; }
        public int CaseTypeId { get; set; }
        public string CaseTypeName { get; set; }
        public string HISStatus { get; set; }
        public int StatusId { get; set; }
        public string CurrentStatus { get; set; }
        [IgnoreParam]
        public DateTime? CrDate { get; set; }
        public string UserName { get; set; }        
        public bool IsLock { get; set; }
        [IgnoreParam]
        public bool IsOutside { get; set; }
    }
}
