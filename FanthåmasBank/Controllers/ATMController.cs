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

        public IActionResult Index(string accountNumber = "", decimal amount = 0)
        {
            ATMViewModel model = new ATMViewModel() { Amount = amount, AccountNumber = accountNumber };
            return View(model);
        }


        [HttpPost]
        public IActionResult Withdraw(string accountNumber, decimal amount)
        {
            Account account = GetAccount(accountNumber);
            if (account != null)
            {
                if (account.Amount < amount)
                {
                    TempData["response"] = "Withdrawl value can't be larger then your current amount. Current amount: " + account.Amount;
                }
                else
                {
                    decimal currentValue = bank.Withdraw(account, amount);
                    TempData["responseSuccess"] = $"New amount: {currentValue}";
                }
            }

            return RedirectToAction("Index", new { accountNumber, amount });
        }

        [HttpPost]
        public IActionResult Deposit(string accountNumber, decimal amount)
        {
            Account account = GetAccount(accountNumber);
            if (account != null)
            {

                if (account.Amount <= 0)
                {
                    TempData["response"] = "Deposit value need to be bigger then 0";
                }
                else
                {
                    decimal currentValue = bank.Deposit(account, amount);
                    TempData["responseSuccess"] = $"New amount: {currentValue}";
                }
            }
            return RedirectToAction("Index", new { accountNumber, amount });
        }

        private Account GetAccount(string accountNumber)
        {
            AllCustomers instance = AllCustomers.Instance();
            List<Account> accounts = new List<Account>();
            Account account = null;

            foreach (var item in instance.Customers)
            {
                foreach (var acc in item.Accounts)
                {
                    if (acc.AccountNumber == accountNumber)
                    {
                        account = acc;
                    }
                }
            }

            // Tips på hur jag kan one-lina båda foreach-looparna uppskattas. 
            // Account account = instance.Customers.Select(c => c.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber)).FirstOrDefault();
            if (account == null)
            {
                TempData["response"] = "The account number you entered was incorrect";
            }
            return account;
        }
    }
}