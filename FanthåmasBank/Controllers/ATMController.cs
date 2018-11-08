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
        BankRepository bank;
        public ATMController()
        {
            bank = new BankRepository();
        }
        public IActionResult Index() => View();
        

        [HttpPost]
        public IActionResult Withdraw(string accountNumber, decimal amount)
        {

            Account account = GetAccount(accountNumber);
            if (account != null)
            {
               decimal currentValue = bank.Withdraw(account, amount);
            }            

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Deposit(string accountNumber, decimal amount)
        {
            Account account = GetAccount(accountNumber);
            if (account != null)
            {
                decimal currentValue = bank.Deposit(account, amount);
            }
            return RedirectToAction("Index");
        }

        private Account GetAccount(string accountNumber)
        {
            AllCustomers instance = AllCustomers.Instance();
            return instance.Customers.Select(c => c.Accounts.FirstOrDefault(x => x.AccountNumber.ToString() == accountNumber)).FirstOrDefault();
        }
    }
}