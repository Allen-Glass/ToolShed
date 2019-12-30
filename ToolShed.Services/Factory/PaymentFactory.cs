using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Payment;

namespace ToolShed.Services.Factory
{
    public class PaymentFactory
    {
        private readonly Dictionary<PaymentProvider, IPaymentServices> paymentServiceReferences;

        public PaymentFactory(Dictionary<PaymentProvider, IPaymentServices> paymentServiceReferences)
        {
            this.paymentServiceReferences = paymentServiceReferences;
        }

        public IPaymentServices GetPaymentReference(PaymentProvider paymentProvider)
        {
            if (paymentServiceReferences.TryGetValue(paymentProvider, out var reference))
                return reference;

            throw new KeyNotFoundException($"This payment reference does not exist, {paymentProvider}");
        }
    }
}
