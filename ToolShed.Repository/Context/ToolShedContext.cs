using Microsoft.EntityFrameworkCore;
using ToolShed.Models.Repository;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Context
{
    public class ToolShedContext : DbContext
    {
        public ToolShedContext(DbContextOptions<ToolShedContext> options) : base(options)
        {
        }

        public virtual DbSet<Address> AddressSet { get; set; }
        public virtual DbSet<Card> CardSet { get; set; }
        public virtual DbSet<CardAddress> CardAddressSet { get; set; }
        public virtual DbSet<Dispenser> DispenserSet { get; set; }
        public virtual DbSet<DispenserTool> DispenserToolSet { get; set; }
        public virtual DbSet<Item> ItemSet { get; set; }
        public virtual DbSet<ItemBundle> ItemBundleSet { get; set; }
        public virtual DbSet<ItemBundleMapping> ItemBundleMappingSet { get; set; }
        public virtual DbSet<ItemDetails> ItemDetailsSet { get; set; }
        public virtual DbSet<ItemRentalDetails> ItemRentalDetailsSet { get; set; }
        public virtual DbSet<ItemType> ItemTypeSet { get; set; }
        public virtual DbSet<MaintenanceProvider> MaintenanceProviderSet { get; set; }
        public virtual DbSet<MaintenanceRequest> MaintenanceRequestSet { get; set; }
        public virtual DbSet<Order> OrderSet { get; set; }
        public virtual DbSet<OrderDetail> OrderDetailsSet { get; set; }
        public virtual DbSet<Rental> RentalSet { get; set; }
        public virtual DbSet<RentalRecord> RentalRecordSet { get; set; }
        public virtual DbSet<User> UserSet { get; set; }
        public virtual DbSet<StateSalesTaxes> StateSalesTaxesSet { get; set; }
        public virtual DbSet<Tenant> TenantSet { get; set; }
        public virtual DbSet<UserAddresses> UserAddressesSet { get; set; }
        public virtual DbSet<UserCard> UserCardSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address")
                .HasKey(c => c.AddressId);
            modelBuilder.Entity<Card>().ToTable("Card")
                .HasKey(c => c.CardId);
            modelBuilder.Entity<CardAddress>().ToTable("CardAddress")
                .HasKey(c => c.CardAddressId);
            modelBuilder.Entity<Dispenser>().ToTable("Dispenser")
                .HasKey(c => c.DispenserId);
            modelBuilder.Entity<DispenserTool>().ToTable("DispenserTool")
                .HasKey(c => c.DispenserToolId);
            modelBuilder.Entity<Item>().ToTable("Item")
                .HasKey(c => c.ItemId);
            modelBuilder.Entity<ItemBundle>().ToTable("ItemBundle")
                .HasKey(c => c.ItemBundleId);
            modelBuilder.Entity<ItemBundleMapping>().ToTable("ItemBundleMapping")
                .HasKey(c => c.ItemBundleMappingId);
            modelBuilder.Entity<ItemDetails>().ToTable("ItemDetails")
                .HasKey(c => c.ItemDetailsId);
            modelBuilder.Entity<ItemRentalDetails>().ToTable("ItemRentalDetails")
                .HasKey(c => c.ItemRentalDetailsId);
            modelBuilder.Entity<ItemType>().ToTable("ItemType")
                .HasKey(c => c.ItemTypeId);
            modelBuilder.Entity<MaintenanceProvider>().ToTable("MaintenanceProvider")
                .HasKey(c => c.MaintenanceProviderId);
            modelBuilder.Entity<MaintenanceRequest>().ToTable("MaintenanceRequest")
                .HasKey(c => c.MaintenanceRequestId);
            modelBuilder.Entity<Order>().ToTable("Order")
                .HasKey(c => c.OrderId);
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails")
                .HasKey(c => c.OrderDetailsId);
            modelBuilder.Entity<Rental>().ToTable("Rental")
                .HasKey(c => c.RentalId);
            modelBuilder.Entity<RentalRecord>().ToTable("RentalRecord")
                .HasKey(c => c.RentalRecordId);
            modelBuilder.Entity<StateSalesTaxes>().ToTable("StateSalesTaxes")
                .HasKey(c => c.StateSalesTaxesId);
            modelBuilder.Entity<Tenant>().ToTable("Tenant")
                .HasKey(c => c.TenantId);
            modelBuilder.Entity<User>().ToTable("User")
                .HasKey(c => c.UserId);
            modelBuilder.Entity<UserAddresses>().ToTable("UserAddress")
                .HasKey(c => c.UserAddressId);
            modelBuilder.Entity<UserCard>().ToTable("UserCard")
                .HasKey(c => c.UserCardId);
        }
    }
}
