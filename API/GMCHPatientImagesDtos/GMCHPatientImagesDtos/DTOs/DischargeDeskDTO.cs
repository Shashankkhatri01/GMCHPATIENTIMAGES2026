using GMCHPatientImagesDtos.DTOs;
using System;
using System.Collections.Generic;
using System.Data;

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
        public string FinalStatus { get; set; }
        public string Remark { get; set; }
        public string MobileNo { get; set; }
        public bool SendNotification { get; set; }
    }

    public class DischargeDeskResponseDTO : BaseDTO
    {
        public DischargeDeskDetailDTO Detail { get; set; }
        public List<DischargeDeskImagesDTO> Images { get; set; }
        public List<PatientUpdateHistoryDTO> PatientUpdateHistoryDTOs { get; set; }
        public List<PatientNotificationHistoryDTO> PatientNotificationHistoryDTOs { get; set; }
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
        public string FinalStatus { get; set; }
        public int CompletePercentage { get; set; }
    }

    public class DischargeDeskImagesDTO
    {
        public long PatientImagesDetailId { get; set; }
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string LocationName { get; set; }
        public string StatusName { get; set; }
        public DateTime? CrDate { get; set; }
        public string UserName { get; set; }
    }

    public class PatientUpdateHistoryDTO
    {
        public string ActionName { get; set; }
        public string ActionTakenBy { get; set; }
        public string ActionTakenFor { get; set; }
        public DateTime? ActionDateTime { get; set; }
        public string FieldName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    public class PatientNotificationHistoryDTO
    {
        public string CampaignName { get; set; }
        public string Destination { get; set; }
        public string TemplateParams     { get; set; }
        public bool IsDelivered { get; set; }
        public int StatusCode { get; set; }
        public string RawResponse { get; set; }
        public string UserName { get; set; }
        public DateTime? CrDate { get; set; }
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
