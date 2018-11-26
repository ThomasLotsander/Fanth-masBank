using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FanthåmasBank.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using FanthåmasBank.Models.ViewModels;

namespace FanthåmasBank.Controllers
{
    public class HomeController : Controller
    {

        private IHostingEnvironment environment;
        private readonly IConfiguration config;

        public HomeController(IHostingEnvironment environment, IConfiguration config)
        {
            this.environment = environment;
            this.config = config;
        }


        public IActionResult Index()
        {

            AllCustomers data = AllCustomers.Instance();
            return View(data.Customers);
        }

        public async Task<IActionResult> SendMail()
        {

            AllCustomers data = AllCustomers.Instance();
            EmailViewModel model = new EmailViewModel()
            {
                Customers = data.Customers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(EmailViewModel model)
        {
            var password = config.GetValue<string>("Email:ApiKey");
            var username = config.GetValue<string>("Email:Username");
            // If Stage / Integration
            if (environment.IsEnvironment("Integration") || environment.IsEnvironment("Stage") || environment.IsDevelopment())
            {

                SmtpClient smtp = new SmtpClient("smtp.mailtrap.io", 2525) // hostname, port
                {

                    // OBS! Skapa konto själv på mailtrap.io och hämta ditt eget username och password.
                    Credentials = new NetworkCredential(username, password), // username, password

                    EnableSsl = true
                };
                smtp.Send
                    ($"ThomasBank@{environment.EnvironmentName}.example.se",
                    model.ToEmail,
                    "Customer info",
                    $"Cusomer name: {model.CustomerName}, customer id: {model.CustomerId}");
            }
            // If Production
            else if (environment.IsProduction())
            {
                var client = new SendGridClient(password);
                var from = new EmailAddress($"ThomasBank@{environment.EnvironmentName}.example.se", "Thomas lotsander");
                var subject = "Sending customer info from Thomas Bank";
                var to = new EmailAddress(model.ToEmail, "Example User");
                var plainTextContent = "Customer info";
                var htmlContent = $"<p>Name: <strong>{model.CustomerName}</strong></p> </br> <p>Id: {model.CustomerId}";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);

            }
            return RedirectToAction("SendMail");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
