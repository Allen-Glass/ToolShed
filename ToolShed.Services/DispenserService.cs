using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Services
{
    public class DispenserService
    {
        private readonly IItemSQLService itemSQLService;
        private readonly IDispenserSQLService dispenserSQLService;

        public DispenserService(IItemSQLService itemSQLService,
            IDispenserSQLService dispenserSQLService)
        {
            this.itemSQLService = itemSQLService;
            this.dispenserSQLService = dispenserSQLService;
        }

        public void AddDispenserToInventoryAsync(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();
        }

        public async Task<string> GetDispenserIotNameAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            return await GetDispenserIotNameAsync(dispenserId);
        }

        public async Task<string> GetDispenserStateAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            return await dispenserSQLService.GetDispenserStateAsync(dispenserId);
        }

        public async Task PurchaseDispenserAsync(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();


        }
    }
}
