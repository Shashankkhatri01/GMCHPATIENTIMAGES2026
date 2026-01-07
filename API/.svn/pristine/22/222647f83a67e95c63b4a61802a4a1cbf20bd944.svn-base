using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using System;
 
using System.IO;
 
using System.Threading.Tasks;

namespace GMCHPatientImages.Utils
{
    public static class PdfHelper
    {

        

        public async static Task<string> GetPdfFileAsync(IConverter converter, IWebHostEnvironment webHostEnvironment, string documentTitle, string fileName, string htmlContent,string folderName)
        {
            var filepath= System.IO.Path.Combine(webHostEnvironment.ContentRootPath, folderName);

            var newpath = System.IO.Path.Combine("PDF", fileName);
            var pdfpath = System.IO.Path.Combine(webHostEnvironment.ContentRootPath, newpath);

            //generate directory if not exists 

            if (!Directory.Exists(filepath))
            {
                 Directory.CreateDirectory(filepath);
               
             }

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                PaperSize = PaperKind.A5,
                Margins = new MarginSettings { Top = 5, Left = 5, Right = 5, Bottom = 5 },
                DocumentTitle = documentTitle,
                Out = pdfpath
            };
            var date = DateTime.Now; 
            var hours = date.TimeOfDay.Hours;
            var minutes = date.TimeOfDay.Minutes;
            var amPmDesignator = "AM";
            if (hours == 0)
                hours = 12;
            else if (hours == 12)
                amPmDesignator = "PM";
            else if (hours > 12)
            {
                hours -= 12;
                amPmDesignator = "PM";
            }
            var formattedDate = String.Format("{0:00} / {1:00} / {2}", date.Day, date.Month, date.Year);
            var formattedTime = String.Format("{0:00} : {1:00} {2}", hours, minutes, amPmDesignator);

            var objectSettings = new ObjectSettings()
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                FooterSettings = { FontSize = 6, Left = "User : " + (1), Center = "Date : " + (formattedDate) + " & " + (formattedTime), Right = "Page [page] of [toPage] ", Line = true },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            converter.Convert(pdf);
           
            //using (var fileStream = new FileStream(globalSettings.Out, FileMode.Open))
            //{
            //    await fileStream.CopyToAsync(memoryStream);
            //}

            return fileName;
        }
    }
}
