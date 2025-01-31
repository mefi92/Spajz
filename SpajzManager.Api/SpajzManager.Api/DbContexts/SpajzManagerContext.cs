using Microsoft.EntityFrameworkCore;
using SpajzManager.Api.Entities;

namespace SpajzManager.Api.DbContexts
{
    public class SpajzManagerContext : DbContext
    {
        public DbSet<Household> Households { get; set; }
        public DbSet<Item> Items { get; set; }

        public SpajzManagerContext(DbContextOptions<SpajzManagerContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Household>()
                .HasData(
                new Household("Városlődi kecó")
                {
                    Id = 1,
                    Description = "Szülői ház"
                },
                new Household("Palotai kégli")
                {
                    Id = 2,
                    Description = "Albérlet"
                },
                new Household("Györöki kisház")
                {
                    Id = 3,
                    Description = "Nyaraló"
                });

            modelBuilder.Entity<Item>()
                .HasData(
                new Item("Alma")
                {
                    Id = 1,
                    HouseholdId = 1,
                    Description = "gyümölcs"
                },
                new Item("Tej")
                {
                    Id = 2,
                    HouseholdId = 1,
                    Description = "egyenesen a tehénből"
                },
                new Item("Brokkoli")
                {
                    Id = 3,
                    HouseholdId = 1,
                    Description = "utlimate zöldség"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
