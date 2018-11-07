using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanthåmasBank.Models
{
    public class BankRepository
    {

        public bool Withdraw(Account account, decimal amount)
        {
            return true;
        }

        public bool Deposit(Account account, decimal amount)
        {
            return true;
        }
    }
}
