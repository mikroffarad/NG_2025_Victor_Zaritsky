using DAL_Core;
using DAL_Core.Entities;
using DAL_Core.Enums;
using HealthCareDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthCareDal.Repositories
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        private readonly PetStoreDbContext _context;

        public VendorRepository(PetStoreDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Vendor?> GetVendorWithDetailsAsync(Guid id)
        {
            return await _context.Vendors
                .Include(v => v.HealthCares)
                .ThenInclude(h => h.Pet)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<ICollection<Vendor>> GetVendorsByContractTypeAsync(ContractType type)
        {
            return await _context.Vendors
                .Where(v => v.ContractType == type)
                .ToListAsync();
        }
    }
}