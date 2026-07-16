using System;
using System.Collections.Generic;

namespace GMCHPatientImagesDtos.DTOs
{
    public class PatientImagesDetailDTO : BaseDTO
    {
        public long PatientImagesDetailId { get; set; }
        public long PatientImagesId { get; set; }
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public string UserName { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string LocationName { get; set; }
        public string StatusName { get; set; }
        public string Remark { get; set; }
        public DateTime? CrDate { get; set; }
    }

    public class PatientImagesDetailResponseDTO : BaseDTO
    {
        public long PatientImagesId { get; set; }        
        public string StatusName { get; set; }
        public List<PatientImagesParticularsDTO> particularsDTOs { get; set; }
    }

    public class PatientImagesParticularsDTO
    {
        public long PatientImagesDetailId { get; set; }
        public string UserName { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string LocationName { get; set; }
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
        public DateTime? CrDate { get; set; }
    }
}
