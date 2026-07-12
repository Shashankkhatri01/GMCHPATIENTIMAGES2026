using ConfigurationDtos.DTOs;
using System;
using System.Collections.Generic;

namespace GMCHPatientImagesDtos.DTOs
{
    public class PatientImagesDetailSaveDTO : BaseDTO
    {
        public long PatientImagesId { get; set; }
        public string Latitute { get; set; }
        public string Longitute { get; set; }
        public string LocationName { get; set; }
        public int StatusId { get; set; }
        public List<PatientImageBulkUploadDTO> Images { get; set; } = new();
    }
    public class PatientImageBulkUploadDTO
    {
        public string ImageName { get; set; }
        public string ImageFull { get; set; }
    }
}
