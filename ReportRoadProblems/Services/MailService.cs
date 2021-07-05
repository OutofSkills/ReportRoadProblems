using Microsoft.Extensions.Options;
using ReportRoadProblems.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ReportRoadProblems.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        // Gets the report and sends it through email
        public void SendEmail(Report report)
        {
            using (var client = new SmtpClient(_mailSettings.Host, _mailSettings.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);

                var email = new MailMessage();
                email.From = new MailAddress(_mailSettings.Mail);
                // Receiver
                email.To.Add(new MailAddress("kojocaru.ivan@gmail.com"));
                email.Subject = "Report - Road Problem";

                email.IsBodyHtml = true;
                email.Body = GetBodyContent(report);
                // Get the picture from the memory stream
                if (report.Picture != null && report.Picture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        report.Picture.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        Attachment att = new Attachment(new MemoryStream(fileBytes), report.Picture.FileName);
                        email.Attachments.Add(att);
                    }
                }

                client.Send(email);
            }
        }

        // Format the body content as HTML and include the report details
        private string GetBodyContent(Report report)
        {
            string bodyContent = "<html>" +
                                    "<span>Buna ziua,</span>" + "<br>" +
                                    "<h3>Acest email a fost trimis automat in vederea raportarii unei gropi la data de " + DateTime.Now + ".</h3>" + "<br>" +
                                    "<b>Gravitatea problemei este raportata ca fiind: " + report.ProblemSeverity + "</b>" +
                                    "<br><br>" +

                                    "<b>Coordonate geospatiale:</b> " + "<br>" +
                                    "<span>Latitudine: " + report.Latitude + "</span> " + "<br>" +
                                    "<span>Longitudine: " + report.Longitude + " .</span>" + "<br>" +
                                    "<span>Adresa: " + report.Address + "</span>" + "<br><br>" +
                                    
                                    "<span>Numai bine,</span>" + "<br>" +
                                    "ReportRoadProblems.ro" +
                                 "</html>";

            return bodyContent;
        }
    }
}
