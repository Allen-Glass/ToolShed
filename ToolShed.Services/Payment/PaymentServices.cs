﻿using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Services.Interfaces.Factory;
using ToolShed.Services.Interfaces.Payment;

namespace ToolShed.Services.Payment
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentFactory paymentFactory;

        public PaymentServices(IPaymentFactory paymentFactory)
        {
            this.paymentFactory = paymentFactory;
        }
    }
}
