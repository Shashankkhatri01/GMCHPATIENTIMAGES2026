using System;

namespace GMCHPatientImagesDtos.DTOs
{
    public class PatientAdmissionImagesDetailDTO : BaseDTO
    {
        public long Id { get; set; }
        public long PatientAdmissionImageId { get; set; }
        public long PatientImagesId { get; set; }
    }

    public class PatientAdmissionImagesDetailResponseDTO
    {
        public long PatientAdmissionImageId { get; set; }
        public long PatientImagesId { get; set; }
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string ImageThumb { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime? CrDate { get; set; }
    }
}
