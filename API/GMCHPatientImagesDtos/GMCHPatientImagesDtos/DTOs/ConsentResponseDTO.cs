using System;

namespace GMCHPatientImagesDtos.DTOs
{
    public class ConsentResponseDTO : BaseDTO
    {
        public long ConsentId { get; set; }
        public string ConsentName { get; set; }
        public string ConsentDescription { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int RecordingTypeId { get; set; }
        public string RecordingTypeName { get; set; }
        public DateTime? RecordingDateTime { get; set; }
    }
}
