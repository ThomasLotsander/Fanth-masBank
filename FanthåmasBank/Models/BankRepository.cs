using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanthåmasBank.Models
{
    public class BankRepository
    {
        public decimal Withdraw(Account account, decimal amount)
        {
            if (account.Amount < amount)
            {
                return account.Amount;
            }
            account.Amount -= amount;
            return account.Amount;
        }

        public decimal Deposit(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                return account.Amount;
            }
            account.Amount += amount;
            return account.Amount;
        }


        public string Transfer(Account senderAccount, Account receiverAccount, decimal amount)
        {
            if (amount > 0)
            {
                var senderAccountAmount_beforeWithdraw = senderAccount.Amount;
                var newSenderAccountAmount = Withdraw(senderAccount, amount);

                if (newSenderAccountAmount == (senderAccountAmount_beforeWithdraw - amount))
                {
                    var newReceiverAccountAmount = Deposit(receiverAccount, amount);
                    return $"Sender:{newSenderAccountAmount},Receiver:{newReceiverAccountAmount}";
                }
            }
            return $"Sender:{senderAccount.Amount},Receiver:{receiverAccount.Amount}";
        }

        public Account GetAccount(string accountNumber)
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

            return account;
        }
    }
}
