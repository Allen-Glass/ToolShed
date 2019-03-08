using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Tools;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class ToolSQLService : IToolSQLService
    {
        private readonly ToolRepository toolRepository;

        public ToolSQLService(ToolRepository toolRepository)
        {
            this.toolRepository = toolRepository;
        }

        public async Task StoreToolAsync(Tool tool)
        {
            if (tool == null)
                throw new ArgumentNullException();

            await toolRepository.AddToolAsync(CreateDtoTool(tool));
        }

        public async Task<Models.Repository.Tool> GetToolAsync(Guid toolId)
        {
            if (toolId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolRepository.GetToolByToolIdAsync(toolId);
        }

        public async Task<IEnumerable<Models.Repository.Tool>> GetToolsAsync(IEnumerable<Guid> toolIds)
        {
            if (toolIds == null)
                throw new ArgumentNullException();

            return await toolRepository.GetToolsByToolIdsAsync(toolIds);
        }

        public async Task DeleteToolAsync(Guid toolId)
        {
            if (toolId == Guid.Empty)
                throw new ArgumentNullException();

            var tool = await toolRepository.GetToolByToolIdAsync(toolId);

            await toolRepository.DeleteToolAsync(tool);
        }

        private Models.Repository.Tool CreateDtoTool(Tool tool)
        {
            return new Models.Repository.Tool
            {
                AssignmentDate = tool.AssignmentDate,
                DecommissionDate = tool.DecommissionDate,
                IsAvailable = tool.IsAvailable,
                IsMissing = tool.IsMissing,
                LastInspection = tool.LastInspection,
                NeedsRepair = tool.NeedsRepair,
                NeedsReplacement = tool.NeedsReplacement,
                PurchaseDate = tool.PurchaseDate,
                ToolCost = tool.ToolCost,
                DispenserId = tool.DispenserId,
                ToolName = tool.ToolName,
                ToolType = tool.ToolType
            };
        }

        private IEnumerable<Models.Repository.Tool> CreateDtoTools(IEnumerable<Tool> tools)
        {
            var toolList = new List<Models.Repository.Tool>();
            foreach (var tool in tools)
            {
                toolList.Add(CreateDtoTool(tool));
            }

            return toolList;
        }
    }
}
