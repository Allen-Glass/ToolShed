using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Dispensers;
using Toolshed.Models.Tools;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class DispenserSQLService : IDispenserSQLService
    {
        private readonly DispenserRepository dispenserRepository;
        private readonly ToolRepository toolRepository;
        private readonly DispenserToolsRepository dispenserToolsRepository;

        public DispenserSQLService(DispenserRepository dispenserRepository
            , ToolRepository toolRepository
            , DispenserToolsRepository dispenserToolsRepository)
        {
            this.dispenserRepository = dispenserRepository;
            this.toolRepository = toolRepository;
            this.dispenserToolsRepository = dispenserToolsRepository;
        }

        public async Task RegisterNewDispenserAsync(Dispenser dispenser)
        {
            await dispenserRepository.AddDispenserAsync(ConvertDispenserToDTODispenser(dispenser));
        }

        public async Task<Dispenser> GetDispenser(Guid dispenserId)
        {
            var dtoDispenser = await dispenserRepository.GetDispenserByDispenserId(dispenserId);
            var dispenserToolIds = await dispenserToolsRepository.GetAllToolsFromDispensery(dtoDispenser.DispenserId);
            var dtoTools = await toolRepository.GetToolsByToolIdsAsync(dispenserToolIds);
            var tools = ConvertDTOToolstoTools(dtoTools);
            return ConvertDTODispenserToDispenser(dtoDispenser, tools);
        }

        public async Task<IEnumerable<Dispenser>> GetAllDispensers()
        {
            var dtoDispensers = await dispenserRepository.GetAllDispensers();
            var dispensers = ConvertDTODispensersToDispensers(dtoDispensers);            
            return dispensers;
        }

        public async Task<IEnumerable<Tool>> GetAllToolsFromDispenser(Guid dispenserId)
        {
            var toolIds = await dispenserToolsRepository.GetAllToolsFromDispensery(dispenserId);
            var dtoTools = await toolRepository.GetToolsByToolIdsAsync(toolIds);

            return ConvertDTOToolstoTools(dtoTools);
        }

        public async Task AddToolToDispenser(Tool tool, Guid dispenserId)
        {
            var toolId = await toolRepository.AddToolAsync(ConvertTooltoDTOTool(tool));
            await dispenserToolsRepository.AddToolToDispensery(CreateDispenserToolObject(dispenserId, toolId));
        }

        public async Task AddToolsToDispenser(IEnumerable<Tool> tools, Guid dispenserId)
        {
            foreach (var tool in tools)
            {
                await AddToolToDispenser(tool, dispenserId);
            }
        }

        private Toolshed.Repository.Models.DispenserTool CreateDispenserToolObject(Guid dispenserId, Guid toolId)
        {
            return new Toolshed.Repository.Models.DispenserTool
            {
                ToolId = toolId,
                DispenserId = dispenserId
            };
        }

        private Toolshed.Repository.Models.Dispenser ConvertDispenserToDTODispenser(Dispenser dispenser)
        {
            return new Toolshed.Repository.Models.Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommissionDate = dispenser.DecommishDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
            };
        }

        private Dispenser ConvertDTODispenserToDispenser(Toolshed.Repository.Models.Dispenser dispenser)
        {
            return new Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommishDate = dispenser.DecommissionDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
            };
        }

        private Dispenser ConvertDTODispenserToDispenser(Toolshed.Repository.Models.Dispenser dispenser, IEnumerable<Tool> tools)
        {
            return new Dispenser
            {
                AvailableTools = tools,
                CreationDate = dispenser.CreationDate,
                DecommishDate = dispenser.DecommissionDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
            };
        }

        private IEnumerable<Dispenser> ConvertDTODispensersToDispensers(IEnumerable<Toolshed.Repository.Models.Dispenser> dispensers)
        {
            var dispenserList = new List<Dispenser>();
            foreach(var dispenser in dispensers)
            {
                ConvertDTODispenserToDispenser(dispenser);
            }

            return dispenserList;
        }

        private Toolshed.Repository.Models.Tool ConvertTooltoDTOTool(Tool tool)
        {
            return new Toolshed.Repository.Models.Tool
            {
                AssignmentDate = tool.AssignmentDate,
                DecommissionDate = tool.DecommissionDate,
                IsAvailable = tool.IsAvailable,
                IsMissing = tool.IsMissing,
                LastInspection = tool.LastInspection,
                NeedsRepair = tool.NeedsRepair,
                NeedsReplacement = tool.NeedsReplacement,
                ToolCost = tool.ToolCost,
                DispenserId = tool.DispenserId,
                ToolName = tool.ToolName,
                ToolType = tool.ToolType,
                PurchaseDate = tool.PurchaseDate
            };
        }

        private IEnumerable<Toolshed.Repository.Models.Tool> ConvertToolstoDTOTools(IEnumerable<Tool> tools)
        {
            var toolList = new List<Toolshed.Repository.Models.Tool>();
            foreach (var tool in tools)
            {
                toolList.Add(ConvertTooltoDTOTool(tool));
            }
            return toolList;
        }

        private Tool ConvertDTOTooltoTool(Toolshed.Repository.Models.Tool tool)
        {
            return new Tool
            {
                ToolId = tool.ToolId,
                AssignmentDate = tool.AssignmentDate,
                DecommissionDate = tool.DecommissionDate,
                IsAvailable = tool.IsAvailable,
                IsMissing = tool.IsMissing,
                LastInspection = tool.LastInspection,
                NeedsRepair = tool.NeedsRepair,
                NeedsReplacement = tool.NeedsReplacement,
                ToolCost = tool.ToolCost,
                DispenserId = tool.DispenserId,
                ToolName = tool.ToolName,
                ToolType = tool.ToolType,
                PurchaseDate = tool.PurchaseDate
            };
        }

        private IEnumerable<Tool> ConvertDTOToolstoTools(IEnumerable<Toolshed.Repository.Models.Tool> tools)
        {
            var toolList = new List<Tool>();
            foreach (var tool in tools)
            {
                toolList.Add(ConvertDTOTooltoTool(tool));
            }
            return toolList;
        }
    }
}
