using System;

namespace GMCHPatientImagesDtos.DTOs
{
    public class DoctorFaceImagesDetailDTO : BaseDTO
    {
        public long Id { get; set; }
        public long DoctorFaceId { get; set; }
        public int DoctorId { get; set; }
    }

    public class DoctorFaceImagesDetailResponseDTO
    {
        public long DoctorFaceId { get; set; }
        public int DoctorId { get; set; }
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime? CrDate { get; set; }
    }
}
