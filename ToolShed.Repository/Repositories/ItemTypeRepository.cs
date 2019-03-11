using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class ItemTypeRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemTypeRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddItemTypeAsync()
        {

        }
    }
}
