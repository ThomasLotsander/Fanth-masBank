﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FanthåmasBank.Models
{
    public class Account
    {
        public Guid AccountNumber { get; set; }
        public decimal Amount { get; set; }

    }
}
