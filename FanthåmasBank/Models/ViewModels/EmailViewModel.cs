using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanthåmasBank.Models.ViewModels
{
    public class EmailViewModel
    {
        public List<Customer> Customers { get; set; }
        public string ToEmail { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }


    }
}
