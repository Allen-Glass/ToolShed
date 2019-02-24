using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Dispensers;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class DispenserSQLService : IDispenserSQLService
    {
        private readonly DispenserRepository dispenserRepository;

        public DispenserSQLService(DispenserRepository dispenserRepository)
        {
            this.dispenserRepository = dispenserRepository;
        }

        public async Task RegisterNewDispenserAsync(Dispenser dispenser)
        {
            await dispenserRepository.AddDispenserAsync(ConvertDispensertoDTODispenser(dispenser));
        }

        private Toolshed.Models.SQL.Dispenser ConvertDispensertoDTODispenser(Dispenser dispenser)
        {
            return new Toolshed.Models.SQL.Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommissionDate = dispenser.DecommishDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
            };
        }
    }
}
