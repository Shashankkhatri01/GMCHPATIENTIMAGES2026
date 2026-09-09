using GMCHPatientImagesDtos.DTOs;
using System.Collections.Generic;

namespace ConfigurationDtos.DTOs
{
    public class DashboardRequestDTO : BaseDTO
    {
        public int PayerId { get; set; }
        public int CaseTypeId { get; set; }
    }

    public class DashboardResponseDTO
    {
        public DashboardCountersDTO Counters { get; set; }
        public List<DashboardStatusListDTO> CategoryStatus { get; set; }
        public List<DashboardTopContributorsListDTO> TopContributors { get; set; }
        public List<DashboardDoctorDepartmentListDTO> DoctorDepartment { get; set; }
        public List<DashboardDoctorStatusListDTO> DoctorStatus { get; set; }
        public List<DashboardDepartmentListDTO> Department { get; set; }
        public List<DashboardDepartmentStatusListDTO> DepartmentStatus { get; set; }
    }
    public class DashboardCountersDTO
    {
        public int TotalImagesUploaded { get; set; }
        public int CurrentIPCount { get; set; }
        public int TodayAdmission { get; set; }
        public decimal UploadPercentage { get; set; }
        public decimal PendingReviewPercentage { get; set; }
    }

    public class DashboardStatusListDTO
    {
        public string StatusName { get; set; }
        public string FontColorCode { get; set; }
        public string BackgroundColorCode { get; set; }
        public int CaseTypeId { get; set; }
        public decimal PhotosUploadedCurrentIPPer { get; set; }
        public decimal PhotosUploadedTodayAdmissionPer { get; set; }
    }

    public class DashboardTopContributorsListDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int PatientsAttended { get; set; }
        public int PatientsWithPhotos { get; set; }
        public decimal PhotosUploadedPercentage { get; set; }
    }
    public class DashboardDoctorDepartmentListDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int IPCount { get; set; }
        public int TodayAdmission { get; set; }
        public int PendingUpload { get; set; }
        public int TotalUpload { get; set; }
        public decimal IPPhotoPer { get; set; }
        public decimal TodayAdmissionPhotoPer { get; set; }
        public string PendingUploadPatientIds { get; set; }
        public List<DashboardDoctorStatusListDTO> Status { get; set; }
        = new List<DashboardDoctorStatusListDTO>();
    }
    public class DashboardDoctorStatusListDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int CaseTypeId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string FontColorCode { get; set; }
        public string BackgroundColorCode { get; set; }
        public decimal IPCountPer { get; set; }
        public decimal TodayAdmissionCountPer { get; set; }
    }
    public class DashboardDepartmentListDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DoctorCount { get; set; }
        public int IPCount { get; set; }
        public int TodayAdmission { get; set; }
        public int PendingUpload { get; set; }
        public int TotalUpload { get; set; }
        public decimal IPPhotoPer { get; set; }
        public decimal TodayAdmissionPhotoPer { get; set; }
        public string PendingUploadPatientIds { get; set; }
        public List<DashboardDepartmentStatusListDTO> Status { get; set; }
            = new List<DashboardDepartmentStatusListDTO>();
    }
    public class DashboardDepartmentStatusListDTO
    {
        public int DepartmentId { get; set; }
        public int CaseTypeId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string FontColorCode { get; set; }
        public string BackgroundColorCode { get; set; }
        public decimal IPCountPer { get; set; }
        public decimal TodayAdmissionCountPer { get; set; }
    }
}
