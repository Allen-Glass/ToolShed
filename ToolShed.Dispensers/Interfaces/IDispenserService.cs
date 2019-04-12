using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Dispensers.Interfaces
{
    public interface IDispenserService
    {
        Task<IEnumerable<ItemBundle>> GetItemBundles();

        Task<IEnumerable<ItemBundle>> GetItemBundles(Guid tenantId);
    }
}
