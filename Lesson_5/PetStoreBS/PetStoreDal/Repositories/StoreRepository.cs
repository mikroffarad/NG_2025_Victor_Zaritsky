using DAL_Core;
using DAL_Core.Entities;
using Microsoft.EntityFrameworkCore;
using PetStoreDal.Repositories.Interfaces;

namespace PetStoreDal.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly PetStoreDbContext _context;

        public StoreRepository(PetStoreDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Store> GetStoreWithDetailsAsync(Guid id)
        {
            return await _context.Stores
                .Include(s => s.Pets)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ICollection<Pet>> GetStorePetsAsync(Guid storeId)
        {
            return await _context.Pets
                .Where(p => p.StoreId == storeId)
                .ToListAsync();
        }
    }
}