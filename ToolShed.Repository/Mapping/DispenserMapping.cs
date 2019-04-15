﻿using System;
using System.Collections.Generic;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class DispenserMapping
    {
        public static Models.Repository.DispenserTool CreateDispenserToolObject(Guid dispenserId, Guid itemId)
        {
            return new Models.Repository.DispenserTool
            {
                ToolId = itemId,
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
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
            };
        }

        public static Dispenser ConvertDtoDispenserToDispenser(Models.Repository.Dispenser dispenser)
        {
            return new Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommishDate = dispenser.DecommissionDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
            };
        }

        public static Dispenser ConvertDtoDispenserToDispenser(Models.Repository.Dispenser dispenser, IEnumerable<Item> items)
        {
            return new Dispenser
            {
                CreationDate = dispenser.CreationDate,
                DecommishDate = dispenser.DecommissionDate,
                DispenserName = dispenser.DispenserName,
                LastMaintenanceCheck = dispenser.LastMaintenanceCheck
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
