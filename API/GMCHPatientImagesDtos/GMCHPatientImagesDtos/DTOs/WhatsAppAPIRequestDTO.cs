using System.Collections.Generic;

namespace GMCHPatientImagesDtos.DTOs
{
    public class WhatsAppAPIRequestDTO
    {
        public long TransactionId { get; set; }
        public string apiKey { get; set; }
        public string campaignName { get; set; }
        public string destination { get; set; }
        public string userName { get; set; }
        public List<string> templateParams { get; set; }
        public string source { get; set; }
        public object media { get; set; }
        public List<WhatsAppButtonDTO> buttons { get; set; }
        public List<object> carouselCards { get; set; }
        public object location { get; set; }
        public object attributes { get; set; }
        public Dictionary<string, string> paramsFallbackValue { get; set; }
    }
    public class WhatsAppButtonDTO
    {
        public string type { get; set; }           // "button"
        public string sub_type { get; set; }       // "url", "quick_reply", etc.
        public int index { get; set; }
        public List<WhatsAppButtonParameterDTO> parameters { get; set; }
    }

    public class WhatsAppButtonParameterDTO
    {
        public string type { get; set; }           // "text"
        public string text { get; set; }           // dynamic text
    }
}
