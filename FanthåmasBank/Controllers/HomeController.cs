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
            var password = config.GetValue<string>("Email:ApiKey");
            var username = config.GetValue<string>("Email:Username");
            // If Stage / Integration
            //if (environment.IsEnvironment("Integration") || environment.IsEnvironment("Stage") || environment.IsDevelopment())
            //{
                
            //    SmtpClient smtp = new SmtpClient("smtp.mailtrap.io", 2525) // hostname, port
            //    {
                    
            //        // OBS! Skapa konto själv på mailtrap.io och hämta ditt eget username och password.
            //        Credentials = new NetworkCredential(username, password), // username, password

            //        EnableSsl = true
            //    };
            //    smtp.Send("FromTest@example.se", "ToTest@example.se", "Rubrik", "content");
            //}
            //// If Production
            //else if (environment.IsProduction()) 
            //{
            //    var client = new SendGridClient(password);
            //    var from = new EmailAddress("thomaslotsander@test.com", "Thomas lotsander");
            //    var subject = "Sending with SendGrid is working";
            //    var to = new EmailAddress("thomaslotsander@gmail.com", "Example User");
            //    var plainTextContent = "and easy to do anywhere, even with C#";
            //    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //    var response = await client.SendEmailAsync(msg);

            //}



            var model = new EmailViewModel();
            model.Apikey = password;
            model.Username = username;

            return View(model);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
