using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ITaxService taxService;
        private readonly IRentalDataService rentalSQLService;

        public PaymentService(ITaxService taxService,
            IRentalDataService rentalSQLService)
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

        public async Task ProcessPendingPaymentsToTenantsAsync(IEnumerable<Payment> payments)
        {
            if (payments == null)
                throw new ArgumentNullException();

            foreach (var payment in payments)
            {
                if (payment.TenantHasBeenPaid == true) //ensure that payments don't get double processed
                {
                    //process payment
                    payment.TenantHasBeenPaid = true;
                }
            }
        }
    }
}
