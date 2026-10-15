using System.Collections.Generic;

namespace GMCHPatientImagesDtos.DTOs
{
    public class PatientAdmissionImagesSaveDTO : BaseDTO
    {
        public long PatientImagesId { get; set; }
        public List<PatientAdmissionImageBulkUploadDTO> Images { get; set; } = new();
    }
    public class PatientAdmissionImageBulkUploadDTO
    {
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
    }
}
