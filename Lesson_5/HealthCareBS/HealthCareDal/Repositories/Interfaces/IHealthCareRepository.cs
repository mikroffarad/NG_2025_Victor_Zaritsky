using DAL_Core.Entities;

namespace HealthCareDal.Repositories.Interfaces
{
    public interface IHealthCareRepository : IRepository<HealthCare>
    {
        Task<HealthCare?> GetHealthCareWithDetailsAsync(Guid id);
        Task<ICollection<HealthCare>> GetHealthCaresByPetAsync(Guid petId);
        Task<ICollection<HealthCare>> GetHealthCaresByVendorAsync(Guid vendorId);
        Task<ICollection<HealthCare>> GetExpiringHealthCaresAsync();
    }
}