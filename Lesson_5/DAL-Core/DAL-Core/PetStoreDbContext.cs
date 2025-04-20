using DAL_Core.Configuration;
using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL_Core;

public class PetStoreDbContext : DbContext
{
    public PetStoreDbContext(DbContextOptions<PetStoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pet> Pets { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<HealthCare> HealthCares { get; set; }
    public DbSet<Vendor> Vendors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PetConfiguration());
        modelBuilder.ApplyConfiguration(new StoreConfiguration());
        modelBuilder.ApplyConfiguration(new HealthCareConfiguration());
        modelBuilder.ApplyConfiguration(new VendorConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
