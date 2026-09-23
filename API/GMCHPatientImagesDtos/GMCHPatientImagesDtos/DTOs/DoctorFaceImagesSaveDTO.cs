using System.Collections.Generic;

namespace GMCHPatientImagesDtos.DTOs
{
    public class DoctorFaceImagesSaveDTO : BaseDTO
    {
        public int DoctorId { get; set; }
        public List<DoctorImageBulkUploadDTO> Images { get; set; } = new();
    }
    public class DoctorImageBulkUploadDTO
    {
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
    }
}
