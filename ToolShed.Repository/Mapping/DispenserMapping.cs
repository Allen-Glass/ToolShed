using System;
using System.Collections.Generic;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class DispenserMapping
    {
        public static Models.Repository.DispenserItem CreateDispenserToolObject(Guid dispenserId, Guid itemId)
        {
            return new Models.Repository.DispenserItem
            {
                ItemId = itemId,
                DispenserId = dispenserId
            };
        }

        public static Models.Repository.Dispenser ConvertDispenserToDtoDispenser(Dispenser dispenser)
        {
            return new Models.Repository.Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommissionDate = dispenser.DecommishDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheckDate = dispenser.LastMaintenanceCheckDate
            };
        }

        public static Dispenser ConvertDtoDispenserToDispenser(Models.Repository.Dispenser dispenser)
        {
            return new Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommishDate = dispenser.DecommissionDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheckDate = dispenser.LastMaintenanceCheckDate
            };
        }

        public static Dispenser ConvertDtoDispenserToDispenser(Models.Repository.Dispenser dispenser, IEnumerable<Item> items)
        {
            return new Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommishDate = dispenser.DecommissionDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheckDate = dispenser.LastMaintenanceCheckDate
            };
        }

        public static IEnumerable<Dispenser> ConvertDtoDispensersToDispensers(IEnumerable<Models.Repository.Dispenser> dispensers)
        {
            var dispenserList = new List<Dispenser>();
            foreach (var dispenser in dispensers)
            {
                ConvertDtoDispenserToDispenser(dispenser);
            }

            return dispenserList;
        }
    }
}
