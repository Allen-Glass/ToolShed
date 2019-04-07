using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.API;

namespace ToolShed.Payments
{
    public class CalculatePayments
    {
        public double CalculateFinalPrice()
        {
            return 0.0;
        }

        public double CalculatePriceWithSale()
        {
            return 0.0;
        }

        public Rental CalculateBasePrice(Rental rental)
        {
            var payment = new Payment();
            var rentalOverdue = rental.RentalReturnTime < rental.RentalDueTime;

            if (rental.RentalDuration < 1)
                rental.RentalDuration = 1;

            payment.BaseCost = rental.Item.PricePerHour * rental.RentalDuration + rental.Item.BaseFee;
            var isMaxPaymentPrice = payment.BaseCost > rental.Item.BuyPrice;

            if (isMaxPaymentPrice)
                payment.BaseCost = rental.Item.BuyPrice;

            rental.Payment = payment;

            return rental;
        }
    }
}
