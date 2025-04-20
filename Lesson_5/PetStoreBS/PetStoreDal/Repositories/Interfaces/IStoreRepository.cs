using DAL_Core.Entities;

namespace PetStoreDal.Repositories.Interfaces
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetStoreWithDetailsAsync(Guid id);
        Task<ICollection<Pet>> GetStorePetsAsync(Guid storeId);
    }
}