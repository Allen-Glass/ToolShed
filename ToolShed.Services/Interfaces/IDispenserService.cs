using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services.Interfaces
{
    public interface IDispenserService
    {
        void AddDispenserToInventoryAsync(Dispenser dispenser);

        Task<string> GetDispenserIotNameAsync(Guid dispenserId);

        Task<string> GetDispenserStateAsync(Guid dispenserId);

        Task PurchaseDispenserAsync(Dispenser dispenser);
    }
}
