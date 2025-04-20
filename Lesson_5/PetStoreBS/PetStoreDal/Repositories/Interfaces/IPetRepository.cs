using DAL_Core.Entities;
using DAL_Core.Enums;

namespace PetStoreDal.Repositories.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<ICollection<Pet>> GetPetsByStoreAsync(Guid storeId);
        Task<ICollection<Pet>> GetPetsByTypeAsync(PetTypes type);
        Task<Pet> GetPetWithDetailsAsync(Guid id);
    }
}