using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanthåmasBank.Models;
using FanthåmasBank.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FanthåmasBank.Controllers
{
    public class ATMController : Controller
    {
        public IActionResult Index()
        {
            ATMViewModel model = new ATMViewModel();
            return View(model);
        }

        [HttpPost]
        public void Withdraw(ATMViewModel model)
        {
            AllCustomers instance = AllCustomers.Instance();

            var account = instance.Customers.Select(c => c.Accounts.FirstOrDefault(x => x.AccountNumber.ToString() == model.AccountNumber)).FirstOrDefault();

            BankRepository reo = new BankRepository();
            reo.Withdraw(account, model.Amount);
        }

        public void Deposit(string accountNumberm, decimal amount)
        {

        }
    }
}