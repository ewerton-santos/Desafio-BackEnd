using Microsoft.EntityFrameworkCore;
using RentBike.Domain.Entities;
using RentBikeUsers.Domain.Entities;
using System.Reflection;

namespace RentBike.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("connection_string");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<DeliverymanUser> DeliverymanUsers { get; set; }
        public DbSet<DriversLicense> DriversLicences { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<RentPlan> RentPlans { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<NotifyOrder> NotifyOrders { get; set; }
    }
}
