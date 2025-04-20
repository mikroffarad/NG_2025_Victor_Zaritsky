using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL_Core.Configuration
{
    public class HealthCareConfiguration : IEntityTypeConfiguration<HealthCare>
    {
        public void Configure(EntityTypeBuilder<HealthCare> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(h => h.TreatmentName).IsRequired().HasMaxLength(200);

            builder.HasOne(h => h.Vendor)
                   .WithMany(v => v.HealthCares)
                   .HasForeignKey(h => h.VendorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
