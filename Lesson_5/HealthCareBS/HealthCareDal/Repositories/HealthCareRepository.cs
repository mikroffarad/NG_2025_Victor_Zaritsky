using DAL_Core;
using DAL_Core.Entities;
using HealthCareDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCareDal.Repositories
{
    public class HealthCareRepository : Repository<HealthCare>, IHealthCareRepository
    {
        private readonly PetStoreDbContext _context;

        public HealthCareRepository(PetStoreDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<HealthCare?> GetHealthCareWithDetailsAsync(Guid id)
        {
            return await _context.HealthCares
                .Include(h => h.Pet)
                .Include(h => h.Vendor)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<ICollection<HealthCare>> GetHealthCaresByPetAsync(Guid petId)
        {
            return await _context.HealthCares
                .Where(h => h.PetId == petId)
                .Include(h => h.Vendor)
                .ToListAsync();
        }

        public async Task<ICollection<HealthCare>> GetHealthCaresByVendorAsync(Guid vendorId)
        {
            return await _context.HealthCares
                .Where(h => h.VendorId == vendorId)
                .Include(h => h.Pet)
                .ToListAsync();
        }

        public async Task<ICollection<HealthCare>> GetExpiringHealthCaresAsync()
        {
            // Get records expiring in the next 30 days
            var thirtyDaysFromNow = DateTime.Now.AddDays(30);

            return await _context.HealthCares
                .Where(h => h.ExpirationDate <= thirtyDaysFromNow && h.ExpirationDate > DateTime.Now)
                .Include(h => h.Pet)
                .Include(h => h.Vendor)
                .ToListAsync();
        }
    }
}