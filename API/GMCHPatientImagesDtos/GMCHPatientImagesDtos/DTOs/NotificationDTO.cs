using GMCHPatientImagesDtos.DTOs;
using System;

namespace ConfigurationDtos.DTOs
{
    public class NotificationDTO : BaseDTO
    {
        public long PatientImagesId { get; set; }
        public string HIS_ID { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string PatientTypeName { get; set; }
        public string PatientName { get; set; }
        public string FinalStatus { get; set; }
        public string Message { get; set; }
        public string Remark { get; set; }
        public bool IsRead { get; set; }
        public string MobileNo { get; set; }
    }
}
