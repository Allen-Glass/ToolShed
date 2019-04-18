using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Dispensers.Interfaces
{
    public interface IDispenserService
    {
        void AddDispenserToInventory(Dispenser dispenser);

        Task<string> GetDispenserIotNameAsync(Guid dispenserId);

        Task<string> GetDispenserStateAsync(Guid dispenserId);

        Task<IEnumerable<ItemBundle>> GetItemBundles();

        Task<IEnumerable<ItemBundle>> GetItemBundles(Guid tenantId);

        Task AddItemToDispenserAsync(Item item);

        Task PurchaseDispenserAsync(Dispenser dispenser);
    }
}
