using System;
using System.Collections.Generic;
using ToolShed.Dispensers.Interfaces;
using ToolShed.Models.Repository;

namespace ToolShed.Dispensers
{
    public class DispenserService : IDispenserService
    {
        public DispenserService()
        {

        }

        public void AddDispenserToInventory(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();


        }

        public bool CheckIfDispenserIsInventory(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            return true;
        }

        public IEnumerable<ItemBundle> GetItemBundles()
        {
            return null;
        }
    }
}
