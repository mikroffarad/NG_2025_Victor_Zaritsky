using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL_Core.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Breed).HasMaxLength(100);
            
            builder.HasOne(p => p.Store)
                   .WithMany(s => s.Pets)
                   .HasForeignKey(p => p.StoreId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.HealthCares)
                   .WithOne(h => h.Pet)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
