using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanthåmasBank.Models
{
    public class AllCustomers
    {
        private static AllCustomers instance;

        public List<Customer> Customers { get; set; } = new List<Customer>();

        private AllCustomers() { }

        public static AllCustomers Instance()
        {
            if(instance == null)
            {
                
                    instance = new AllCustomers();
            }
            return instance;
           
        }


    }
}
