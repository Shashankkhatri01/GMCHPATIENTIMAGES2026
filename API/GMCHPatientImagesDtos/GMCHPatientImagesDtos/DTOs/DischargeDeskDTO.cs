using GMCHPatientImagesDtos.DTOs;
using System;
using System.Collections.Generic;

namespace ConfigurationDtos.DTOs
{
    public class DischargeDeskRequestDTO : BaseDTO
    {
        public long ID { get; set; }
        public long PatientImagesId { get; set; }
        public string HIS_ID { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public int PatientTypeId { get; set; }
        public string PatientName { get; set; }
    }

    public class DischargeDeskResponseDTO : BaseDTO
    {
        public DischargeDeskDetailDTO Detail { get; set; }
        public List<DischargeDeskImagesDTO> Images { get; set; }
    }

    public class DischargeDeskDetailDTO
    {
        public long PatientImagesId { get; set; }
        public string HIS_ID { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string PatientTypeName { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string PayerName { get; set; }
        public string WardName { get; set; }
        public string BedNumber { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string CaseTypeName { get; set; }
        public bool IsOutside { get; set; }
        public string HISStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string Remark { get; set; }
        public DateTime? CrDate { get; set; }
        public string UserName { get; set; }
        public bool IsLock { get; set; }
    }

    public class DischargeDeskImagesDTO
    {
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string LocationName { get; set; }
        public string StatusName { get; set; }
    }

    public class DischargeDeskFullDetailDTO
    {
        public long PatientImagesId { get; set; }
        public string HIS_ID { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string PatientTypeName { get; set; }
        public string PatientName { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string PayerName { get; set; }
        public string WardName { get; set; }
        public string BedNumber { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string CaseTypeName { get; set; }
        public bool IsOutside { get; set; }
        public string HISStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string Remark { get; set; }
        public DateTime? CrDate { get; set; }
        public string UserName { get; set; }
        public bool IsLock { get; set; }
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string LocationName { get; set; }
        public string StatusName { get; set; }
    }
}
