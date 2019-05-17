using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Models.Enums;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Services.Dispensers
{
    /// <summary>
    /// manage items in a dispenser
    /// </summary>
    public class DispenserItemsService
    {
        private readonly IItemDataService itemSQLService;
        private readonly IDispenserDataService dispenserSQLService;

        public DispenserItemsService(IItemDataService itemSQLService,
            IDispenserDataService dispenserSQLService)
        {
            this.itemSQLService = itemSQLService;
            this.dispenserSQLService = dispenserSQLService;
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundles()
        {
            return await itemSQLService.GetItemBundlesAsync();
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundles(Guid tenantId)
        {
            return await itemSQLService.GetItemBundlesAsync(tenantId);
        }

        public async Task AddItemToDispenserAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            await dispenserSQLService.AddItemToDispenserAsync(item);
        }

        public async Task MarkItemAsRentedAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            item.ItemState = ItemState.Reserved;
        }
    }
}
