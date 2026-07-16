namespace GMCHPatientImagesDtos.DTOs
{
    public class WhatsAppLogRequestDTO : BaseDTO
    {
        public long ID { get; set; }
        public string BabyName { get; set; }
        public string CampaignName { get; set; }
        public string Destination { get; set; }
        public string MessageBody { get; set; }
        public string TemplateParams { get; set; }
        public bool IsDelivered { get; set; }
        public int StatusCode { get; set; }
        public string RawResponse { get; set; }
        public string Message { get; set; }
    }
}
