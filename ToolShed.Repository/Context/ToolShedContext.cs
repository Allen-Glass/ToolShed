using Microsoft.EntityFrameworkCore;
using Toolshed.Models.Dispensers;
using Toolshed.Models.Maintenance;
using Toolshed.Models.User;

namespace ToolShed.Repository.Context
{
    public class ToolShedContext : DbContext
    {
        public ToolShedContext(DbContextOptions<ToolShedContext> options) : base(options)
        {
        }

        public virtual DbSet<Dispenser> DispenserSet { get; set; }
        public virtual DbSet<DispensibleContainers> DispensibleContainersSet { get; set; }
        public virtual DbSet<MaintenanceProvider> MaintenanceProviderSet { get; set; }
        public virtual DbSet<MaintenanceRequest> MaintenanceRequestSet { get; set; }
        public virtual DbSet<Address> AddressSet { get; set; }
        public virtual DbSet<Card> CardSet { get; set; }
        public virtual DbSet<User> UserSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dispenser>().ToTable("Dispenser")
                .HasKey(c => c.DispenserId);
            modelBuilder.Entity<DispensibleContainers>().ToTable("Dispenser")
                .HasKey(c => c.DispensibleContainersId);
            modelBuilder.Entity<MaintenanceProvider>().ToTable("MaintenanceProvider")
                .HasKey(c => c.MaintenanceProviderId);
            modelBuilder.Entity<MaintenanceRequest>().ToTable("MaintenanceRequest")
                .HasKey(c => c.MaintenanceRequestId);
            modelBuilder.Entity<Address>().ToTable("Address")
                .HasKey(c => c.AddressId);
            modelBuilder.Entity<Card>().ToTable("Card")
                .HasKey(c => c.CardId);
            modelBuilder.Entity<User>().ToTable("User")
                .HasKey(c => c.UserId);
        }
    }
}
