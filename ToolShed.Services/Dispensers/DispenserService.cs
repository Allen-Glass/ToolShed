using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services.Dispensers
{
    public class DispenserService : IDispenserService
    {
        private readonly IItemDataService itemSQLService;
        private readonly IDispenserDataService dispenserSQLService;

        public DispenserService(IItemDataService itemSQLService,
            IDispenserDataService dispenserSQLService)
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
