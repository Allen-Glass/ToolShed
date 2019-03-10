using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class ToolRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ToolRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddToolAsync(Tool tool)
        {
            await toolShedContext.ToolSet
                .AddAsync(tool);
            await toolShedContext.SaveChangesAsync();

            return tool.ToolId;
        }

        public async Task<Tool> GetToolByToolIdAsync(Guid toolId)
        {
            return await toolShedContext.ToolSet
                .FirstOrDefaultAsync(c => c.ToolId.Equals(toolId));
        }

        public async Task<IEnumerable<Tool>> GetToolsByToolIdsAsync(IEnumerable<Guid> toolId)
        {
            var toolsList = new List<Tool>();
            foreach (var id in toolId)
            {
                var tool = await GetToolByToolIdAsync(id);
                toolsList.Add(tool);
            }

            return toolsList;
        }

        public async Task DeleteToolAsync(Tool tool)
        {
            toolShedContext.ToolSet
                .Remove(tool);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
