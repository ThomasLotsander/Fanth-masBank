using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FanthåmasBank.Models;

namespace FanthåmasBank.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            AllCustomers data = AllCustomers.Instance();
            return View(data.Customers);
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
