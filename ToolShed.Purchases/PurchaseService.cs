using System;
using ToolShed.Models.API;

namespace ToolShed.Purchases
{
    public class PurchaseService
    {
        public PurchaseService()
        {

        }

        public void PurchaseItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();


        }

        public void PurchaseDispenser(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();


        }
    }
}
