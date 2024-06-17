using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PremiumEvents.API.Data
{
    public class EventsAuthDbContext: IdentityDbContext
    {
        public EventsAuthDbContext(DbContextOptions<EventsAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var clientRoleId = "da32c01f-2646-437d-bf89-ed46a474f8a7";
            var adminRoleId = "8a2af93a-ee55-47be-93fd-f8339966fa7c";
            var masterRoleId = "9d4dc3a0-618e-4649-b878-382788e6e5e2";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = clientRoleId,
                    ConcurrencyStamp = clientRoleId,
                    Name = "Client",
                    NormalizedName = "Client".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = masterRoleId,
                    ConcurrencyStamp = masterRoleId,
                    Name = "Master",
                    NormalizedName = "Master".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
