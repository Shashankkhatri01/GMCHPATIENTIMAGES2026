using DinkToPdf;
using DinkToPdf.Contracts;
using GMCHPatientImagesDtos.DTOs;
using Microsoft.AspNetCore.Hosting;
using System;
 
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GMCHPatientImages.Utils
{
    public static class EmailHelper
    {
       
        public async static Task<string> GetEmailAsync(MailMessage mailMessage, AppSettings _appSettings)
        { 
            using (SmtpClient client = new SmtpClient())
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_appSettings.EmailFrom, _appSettings.SmtpPass);//"pushkarlaldangi96@gmail.com", "push@1567482930"
                client.EnableSsl = true;
                client.Host = _appSettings.SmtpHost;   //"smtp.gmail.com";
                client.Port = _appSettings.SmtpPort;
                //client.TargetName = "noreply@gmail.com";//587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(mailMessage);
            }

            return "";
        }
    }
}
