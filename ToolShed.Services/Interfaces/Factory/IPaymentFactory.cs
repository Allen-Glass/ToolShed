using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Payment;

namespace ToolShed.Services.Interfaces.Factory
{
    public interface IPaymentFactory
    {
        IPaymentServices GetPaymentReference(PaymentProvider paymentProvider);
    }
}
