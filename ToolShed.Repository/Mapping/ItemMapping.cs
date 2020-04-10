using System.Collections.Generic;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class ItemMapping
    {
        public static Models.Repository.Item CreateDtoItem(this Item item)
        {
            return new Models.Repository.Item
            {
                SalePrice = item.SalePrice,
                BuyPrice = item.BuyPrice,
                IsAvailable = item.IsAvailable,
                IsDamaged = item.IsDamaged,
                IsRentable = item.IsRentable,
                DisplayName = item.DisplayName,
                TenantId = item.TenantId,
                DispenserId = item.DispenserId
            };
        }

        public static IEnumerable<Models.Repository.Item> CreateDtoItems(this IEnumerable<Item> items)
        {
            var itemList = new List<Models.Repository.Item>();
            foreach (var item in items)
            {
                itemList.Add(CreateDtoItem(item));
            }
            return itemList;
        }

        public static Models.Repository.ItemBundle CreateDtoItemBundle(this ItemBundle itemBundle)
        {
            return new Models.Repository.ItemBundle
            {
                DisplayName = itemBundle.DisplayName,
                TenantId = itemBundle.TenantId
            };
        }

        public static Models.Repository.ItemRentalDetails CreateItemRentalDetails(this ItemRentalDetails itemRentalDetails)
        {
            return new Models.Repository.ItemRentalDetails
            {
                BaseRentalFee = itemRentalDetails.BaseRentalFee,
                ItemId = itemRentalDetails.Item.ItemId,
                PricePerHour = itemRentalDetails.PricePerHour
            };
        }

        public static Item ConvertDtoItemToItem(this Models.Repository.Item item)
        {
            return new Item
            {
                ItemId = item.ItemId,
                SalePrice = item.SalePrice,
                BuyPrice = item.BuyPrice,
                IsAvailable = item.IsAvailable,
                IsDamaged = item.IsDamaged,
                IsRentable = item.IsRentable,
                DisplayName = item.DisplayName,
                TenantId = item.TenantId,
                DispenserId = item.DispenserId
            };
        }

        public static IEnumerable<Item> ConvertDtoItemstoItems(this IEnumerable<Models.Repository.Item> items)
        {
            var itemList = new List<Item>();
            foreach (var item in items)
            {
                itemList.Add(ConvertDtoItemToItem(item));
            }
            return itemList;
        }

        public static ItemBundle ConvertDtoItemBundleToItemBundle(this Models.Repository.ItemBundle itemBundle)
        {
            return new ItemBundle
            {
                DisplayName = itemBundle.DisplayName,
                ItemBundleId = itemBundle.ItemBundleId,
                TenantId = itemBundle.TenantId
            };
        }

        public static IEnumerable<ItemBundle> ConvertDtoItemBundlesToItemBundles(this IEnumerable<Models.Repository.ItemBundle> itemBundles)
        {
            var itemBundlesList = new List<ItemBundle>();
            foreach (var itemBundle in itemBundles)
            {
                itemBundlesList.Add(ConvertDtoItemBundleToItemBundle(itemBundle));
            }

            return itemBundlesList;
        }

        public static ItemRentalDetails ConvertItemRentalDetails(this Models.Repository.ItemRentalDetails itemRentalDetails, Models.Repository.Item item)
        {
            return new ItemRentalDetails
            {
                ItemRentalDetailsId = itemRentalDetails.ItemRentalDetailsId,
                BaseRentalFee = itemRentalDetails.BaseRentalFee,
                Item = ConvertDtoItemToItem(item),
                PricePerHour = itemRentalDetails.PricePerHour
            };
        }
    }
}
