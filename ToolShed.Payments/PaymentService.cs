using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Payments.Interfaces;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Payments
{
    public class PaymentService
    {
        private readonly ITaxService taxService;
        private readonly IRentalSQLService rentalSQLService;

        public PaymentService(ITaxService taxService,
            IRentalSQLService rentalSQLService)
        {
            this.taxService = taxService;
            this.rentalSQLService = rentalSQLService;
        }

        public async Task<Rental> CalculateRentalPriceAsync(Rental rental, string state)
        {
            var payment = rental.Payment;
            var rentalOverdue = rental.RentalReturnTime < rental.RentalDueTime;

            if (rental.RentalDuration < 1)
                rental.RentalDuration = 1;

            payment.PreTaxTotalCost = rental.ItemRentalDetails.PricePerHour * rental.RentalDuration + payment.BaseRentalFee;
            payment.BaseRentalFee = rental.ItemRentalDetails.BaseRentalFee;
            payment.ClientPercentage = 0;
            payment.ClientPay = payment.PreTaxTotalCost * payment.ClientPay;
            payment.ToolShedPay = payment.PreTaxTotalCost - payment.ClientPay;
            payment.SalesTaxCost = await taxService.GetSalesTaxAsync(payment, state);
            payment.TotalCost = payment.PreTaxTotalCost + payment.SalesTaxCost;

            var isMaxPaymentPrice = (payment.BaseRentalFee * payment.ClientPercentage > rental.ItemRentalDetails.Item.BuyPrice);

            if (isMaxPaymentPrice)
                payment.TotalCost = rental.ItemRentalDetails.Item.BuyPrice;

            rental.Payment = payment;

            return rental;
        }
    }
}
