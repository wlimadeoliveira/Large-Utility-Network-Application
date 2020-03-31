using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace Angle.Controllers
{
    public class EmailSendController : Controller
    {
        public async Task<IActionResult> Index(string pwtoken)
        {
            var apiKey = "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"; //Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@precisionwave.com", "No Reply");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("wagner.lima@precisionwave.com", "W Lima");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
  
            return View("~/Views/Home/Index.cshtml");
        }
    }
}