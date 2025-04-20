using DAL_Core;
using DAL_Core.Entities;
using DAL_Core.Enums;
using Microsoft.EntityFrameworkCore;
using PetStoreDal.Repositories.Interfaces;

namespace PetStoreDal.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private readonly PetStoreDbContext _context;

        public PetRepository(PetStoreDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<Pet>> GetPetsByStoreAsync(Guid storeId)
        {
            return await _context.Pets
                .Where(p => p.StoreId == storeId)
                .ToListAsync();
        }

        public async Task<ICollection<Pet>> GetPetsByTypeAsync(PetTypes type)
        {
            return await _context.Pets
                .Where(p => p.Type == type)
                .ToListAsync();
        }

        public async Task<Pet> GetPetWithDetailsAsync(Guid id)
        {
            return await _context.Pets
                .Include(p => p.Store)
                .Include(p => p.HealthCares)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}