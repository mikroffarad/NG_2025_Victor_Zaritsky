using PetStoreBL.Models;

namespace PetStoreBL.Services.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllStores();
        Task<StoreDto> GetStoreById(Guid id);
        Task<Guid> UpdateStore(Guid id, StoreDto storeDto);
        Task<IEnumerable<PetDto>> GetStorePets(Guid storeId);
        Task<IEnumerable<object>> GetStoreHealthcareRecords(Guid storeId);
    }
}