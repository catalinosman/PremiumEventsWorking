using Microsoft.EntityFrameworkCore;
using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Data
{
    public class PremiumEventsDbContext : DbContext
    {
        public PremiumEventsDbContext(DbContextOptions<PremiumEventsDbContext> options) : base(options)
        {
        }
        public DbSet<County> Counties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<CityServiceCategory> CityServiceCategories { get; set; }
        public DbSet<CityService> CityServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityServiceCategory>()
                .HasKey(csc => new { csc.CityId, csc.ServiceCategoryId });

            modelBuilder.Entity<CityServiceCategory>()
                .HasOne(csc => csc.City)
                .WithMany(c => c.CityServiceCategories)
                .HasForeignKey(csc => csc.CityId);

            modelBuilder.Entity<CityServiceCategory>()
                .HasOne(csc => csc.ServiceCategory)
                .WithMany(sc => sc.CityServiceCategories)
                .HasForeignKey(csc => csc.ServiceCategoryId);

            modelBuilder.Entity<CityService>()
                .HasKey(cs => new { cs.CityId, cs.ServiceId });

            modelBuilder.Entity<CityService>()
                .HasOne(cs => cs.City)
                .WithMany(c => c.CityServices)
                .HasForeignKey(cs => cs.CityId);

            modelBuilder.Entity<CityService>()
                .HasOne(cs => cs.Service)
                .WithMany(s => s.CityServices)
                .HasForeignKey(cs => cs.ServiceId);

        }
    }
}

