using System;

namespace GMCHPatientImagesDtos.DTOs
{
    public class ConsentRequestDTO : BaseDTO
    {
        public long ConsentId { get; set; }
        public string ConsentName { get; set; }
        public string ConsentDescription { get; set; }
        public int DepartmentId { get; set; }
        public int DoctorId { get; set; }
        public int RecordingTypeId { get; set; }
        public DateTime? RecordingDateTime { get; set; }
    }
}
