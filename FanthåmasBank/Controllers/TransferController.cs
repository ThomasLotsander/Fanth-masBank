using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FanthåmasBank.Models;
using FanthåmasBank.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FanthåmasBank.Controllers
{
    public class TransferController : Controller
    {

        private BankRepository bank;
        public TransferController()
        {
            bank = new BankRepository();
        }

        public IActionResult Index(string senderAccountNumber = "", string receiverAccountNumber = "", decimal amount = 0)
        {
            TransferViewModel model = new TransferViewModel()
            {
                Amount = amount,
                SenderAccountNumber = senderAccountNumber,
                ReceiverAccountNumber = receiverAccountNumber
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Transfer(string senderAccountNumber, string receiverAccountNumber, decimal amount)
        {

            Account senderAccount = bank.GetAccount(senderAccountNumber);
            Account receiverAccount = bank.GetAccount(receiverAccountNumber);

            if (senderAccount != null && receiverAccount != null)
            {
                if (senderAccount.Amount < amount)
                {
                    TempData["response"] = "Senders account amount must be larger then the send amount. Current amount: " + senderAccount.Amount;
                }
                else
                {
                    bank.Transfer(senderAccount, receiverAccount, amount);
                    TempData["responseSuccess"] = $"Sender Account new amount: {senderAccount.Amount} , Receiver account new amount: {receiverAccount.Amount}";
                }
            }
            else if (senderAccount == null)
            {
                TempData["response"] = "The sender account number you entered was incorrect";
            }
            else
            {
                TempData["response"] = "The receiver account number you entered was incorrect";
            }
            return RedirectToAction("Index", new { senderAccount, receiverAccount });
        }
    }
}