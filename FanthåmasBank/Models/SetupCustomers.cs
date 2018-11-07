using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanthåmasBank.Models
{
    public static class SetupCustomers
    {

        public static void SetUpCusomer()
        {
            AllCustomers instace = AllCustomers.Instance();

            instace.Customers.Add(
                new Customer()
                {
                    Id = 1,
                    Name = "Thomas",
                    Accounts = new List<Account>(){
                        new Account(){
                            AccountNumber = Guid.NewGuid(),
                            Amount = 100},
                        new Account()
                        {
                            AccountNumber = Guid.NewGuid(),
                            Amount = 200
                        }
                    }
                });

            instace.Customers.Add(
                new Customer()
                {
                    Id = 2,
                    Name = "Micke",
                    Accounts = new List<Account>(){
                        new Account(){
                            AccountNumber = Guid.NewGuid(),
                            Amount = 13123},
                     new Account()
                        {
                            AccountNumber = Guid.NewGuid(),
                            Amount = 43
                        }
                    }
                });

            instace.Customers.Add(
                new Customer()
                {
                    Id = 3,
                    Name = "Markus",
                    Accounts = new List<Account>(){
                        new Account(){
                            AccountNumber = Guid.NewGuid(),
                            Amount = 10000},
                     new Account()
                        {
                            AccountNumber = Guid.NewGuid(),
                            Amount = 5124
                        }
                    }
                });

            instace.Customers.Add(
            new Customer()
            {
                Id = 4,
                Name = "Jocke",
                Accounts = new List<Account>(){
                        new Account(){
                            AccountNumber = Guid.NewGuid(),
                            Amount = 23},
                        new Account()
                        {
                            AccountNumber = Guid.NewGuid(),
                            Amount = 1515
                        }
                }
            });

        }
    }
}
