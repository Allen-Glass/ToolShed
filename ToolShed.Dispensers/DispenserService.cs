﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Dispensers.Interfaces;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Dispensers
{
    public class DispenserService : IDispenserService
    {
        private readonly IItemSQLService itemSQLService;
        private readonly IDispenserSQLService dispenserSQLService;

        public DispenserService(IItemSQLService itemSQLService,
            IDispenserSQLService dispenserSQLService)
        {
            this.itemSQLService = itemSQLService;
            this.dispenserSQLService = dispenserSQLService;
        }

        public void AddDispenserToInventory(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundles()
        {
            return await itemSQLService.GetItemBundlesAsync();
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundles(Guid tenantId)
        {
            return await itemSQLService.GetItemBundlesAsync(tenantId);
        }

        public async Task AddItemToDispenserAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            await dispenserSQLService.AddItemToDispenserAsync(item, item.DispenserId);
        }

        public async Task PurchaseDispenserAsync(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();


        }
    }
}
