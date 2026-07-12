using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesDtos.DTOs
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string JwtTokenDuration { get; set; }
        public string RefreshTokenDuration { get; set; }
        public string URL { get; set; }
        public string ReportPath { get; set; }
        public string HeaderPath { get; set; }
        public string FooterPath { get; set; }
        public string LogoPath { get; set; }
        public string GoogleAPIKey { get; set; }
    }
}
