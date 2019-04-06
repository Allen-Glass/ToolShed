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

        }

        public double CalculatePreTaxPrice()
        {

        }

        public double CalculatePreSalePrice(Rental rental, int rentalDuration)
        {
            var timeOverdue = rental.

            if (rentalDuration < 1)
                rentalDuration = 1;

            var price = rental.Item.PricePerHour * rentalDuration;

            if ()

            if (price > rental.Item.BuyPrice)
                price = rental.Item.BuyPrice;

            return price;
        }
    }
}
