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
            return account.Amount - amount;
        }

        public decimal Deposit(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                return account.Amount;
            }
            return account.Amount + amount;
        }
    }
}
