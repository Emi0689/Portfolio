using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SendReply(string Contact_Name, string Contact_Email, string Contact_Subject, string Contact_Text)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(Contact_Email);
                mail.To.Add(Constantes.emailenvio.ToString());
                mail.Subject = "Portfolio Mio " + Contact_Subject;
                mail.Body = "<html><body>"+ Contact_Name + " <br /> " + Contact_Text;
                mail.IsBodyHtml = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential(Constantes.emailenvio.ToString(), Constantes.credencialenvio.ToString());
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                SmtpServer.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
