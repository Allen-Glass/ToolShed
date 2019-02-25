using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.SQL;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class DispenserToolsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public DispenserToolsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddToolToDispensery(DispenserTool dispenserTool)
        {
            await toolShedContext.DispenserToolSet
                .AddAsync(dispenserTool);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<Guid> GetDispenserByToolId(Guid toolId)
        {
            var dispenserTool = await toolShedContext.DispenserToolSet
                .FirstOrDefaultAsync(c => c.ToolId.Equals(toolId));

            return dispenserTool.DispenserId;
        }

        public async Task<IEnumerable<Guid>> GetAllToolsFromDispensery(Guid dispenserId)
        {
            return await toolShedContext.DispenserToolSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.ToolId)
                .ToListAsync();
        }

        public async Task RemoveToolFromDispensery(DispenserTool dispenserTool)
        {
            toolShedContext.DispenserToolSet
                .Remove(dispenserTool);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
