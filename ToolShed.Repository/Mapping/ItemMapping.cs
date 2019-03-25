using System.Collections.Generic;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class ItemMapping
    {
        public static Models.Repository.Item CreateDtoItem(Item item)
        {
            return new Models.Repository.Item
            {
                SalePrice = item.SalePrice,
                BaseRentalPeriodDuration = item.BaseRentalPeriodDuration,
                BuyPrice = item.BuyPrice,
                IsAvailable = item.IsAvailable,
                IsDamaged = item.IsDamaged,
                IsRentable = item.IsRentable,
                ItemName = item.ItemName,
                PricePerHourOver = item.PricePerHourOver,
                PricePerHour = item.PricePerHour,
                TenantId = item.TenantId,
                DispenserId = item.DispenserId
            };
        }

        public static IEnumerable<Models.Repository.Item> CreateDtoItems(IEnumerable<Item> items)
        {
            var itemList = new List<Models.Repository.Item>();
            foreach (var item in items)
            {
                itemList.Add(CreateDtoItem(item));
            }
            return itemList;
        }

        public static Item ConvertDtoItemToItem(Models.Repository.Item item)
        {
            return new Item
            {
                ItemId = item.ItemId,
                SalePrice = item.SalePrice,
                BaseRentalPeriodDuration = item.BaseRentalPeriodDuration,
                BuyPrice = item.BuyPrice,
                IsAvailable = item.IsAvailable,
                IsDamaged = item.IsDamaged,
                IsRentable = item.IsRentable,
                ItemName = item.ItemName,
                PricePerHourOver = item.PricePerHourOver,
                PricePerHour = item.PricePerHour,
                TenantId = item.TenantId,
                DispenserId = item.DispenserId
            };
        }

        public static IEnumerable<Item> ConvertDtoItemstoItems(IEnumerable<Models.Repository.Item> items)
        {
            var itemList = new List<Item>();
            foreach (var item in items)
            {
                itemList.Add(ConvertDtoItemToItem(item));
            }
            return itemList;
        }
    }
}
