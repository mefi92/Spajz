using Microsoft.EntityFrameworkCore;
using SpajzManager.Api.Entities;

namespace SpajzManager.Api.DbContexts
{
    public class SpajzManagerContext : DbContext
    {
        public DbSet<Household> Households { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageType> StorageTypes { get; set; }


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
                        .Property(i => i.CreatedAt)
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<StorageType>().HasData(
                new StorageType { Id = 1, Name = "Ideiglenes tároló" },
                new StorageType { Id = 2, Name = "Hűtő" },
                new StorageType { Id = 3, Name = "Fagyasztó" },
                new StorageType { Id = 4, Name = "Tároló "},
                new StorageType { Id = 5, Name = "Spájz" },
                new StorageType { Id = 6, Name = "Pince" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
