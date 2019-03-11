using Microsoft.EntityFrameworkCore;
using ToolShed.Models.Repository;

namespace ToolShed.Repository.Context
{
    public class ToolShedContext : DbContext
    {
        public ToolShedContext(DbContextOptions<ToolShedContext> options) : base(options)
        {
        }

        public virtual DbSet<Dispenser> DispenserSet { get; set; }
        public virtual DbSet<DispenserTool> DispenserToolSet { get; set; }
        public virtual DbSet<MaintenanceProvider> MaintenanceProviderSet { get; set; }
        public virtual DbSet<MaintenanceRequest> MaintenanceRequestSet { get; set; }
        public virtual DbSet<Address> AddressSet { get; set; }
        public virtual DbSet<Card> CardSet { get; set; }
        public virtual DbSet<UserCard> UserCardSet { get; set; }
        public virtual DbSet<UserAddresses> UserAddressesSet { get; set; }
        public virtual DbSet<CardAddress> CardAddressesSet { get; set; }
        public virtual DbSet<User> UserSet { get; set; }
        public virtual DbSet<Item> ItemSet { get; set; }
        public virtual DbSet<Rental> RentalSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispenser>().ToTable("Dispenser")
                .HasKey(c => c.DispenserId);
            modelBuilder.Entity<DispenserTool>().ToTable("DispenserTool")
                .HasKey(c => c.DispenserToolId);
            modelBuilder.Entity<MaintenanceProvider>().ToTable("MaintenanceProvider")
                .HasKey(c => c.MaintenanceProviderId);
            modelBuilder.Entity<MaintenanceRequest>().ToTable("MaintenanceRequest")
                .HasKey(c => c.MaintenanceRequestId);
            modelBuilder.Entity<Address>().ToTable("Address")
                .HasKey(c => c.AddressId);
            modelBuilder.Entity<Card>().ToTable("Card")
                .HasKey(c => c.CardId);
            modelBuilder.Entity<UserCard>().ToTable("UserCard")
                .HasKey(c => c.UserCardId);
            modelBuilder.Entity<UserAddresses>().ToTable("UserAddresses")
                .HasKey(c => c.UserAddressesId);
            modelBuilder.Entity<CardAddress>().ToTable("CardAddresses")
                .HasKey(c => c.CardAddressesId);
            modelBuilder.Entity<User>().ToTable("User")
                .HasKey(c => c.UserId);
            modelBuilder.Entity<Item>().ToTable("Item")
                .HasKey(c => c.ItemId);
            modelBuilder.Entity<Rental>().ToTable("Rental")
                .HasKey(c => c.RentalId);
        }
    }
}
