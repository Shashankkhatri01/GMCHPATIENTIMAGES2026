using System.Collections.Generic;

namespace GMCHPatientImagesDtos.DTOs
{
    public class ImageValidationResultDTO
    {
        public string ImageName { get; set; }
        public bool IsValid { get; set; }
        public bool DoctorVerified { get; set; }
        public decimal? DoctorSimilarity { get; set; }
        public bool PatientVerified { get; set; }
        public decimal? PatientSimilarity { get; set; }
        public List<string> Errors { get; set; } = new();
    }

    public class FaceMatchResultDTO    
    {
        public bool IsVerified { get; set; }
        public decimal Similarity { get; set; }
        public long ReferenceId { get; set; }
        public string ReferenceImageName { get; set; }
    }
}
