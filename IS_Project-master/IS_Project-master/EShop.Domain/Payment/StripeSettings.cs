﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Payment
{
    public class StripeSettings
    {
        public string? SecretKey { get; set; }
        public string? PublishableKey { get; set; }
    }

}
