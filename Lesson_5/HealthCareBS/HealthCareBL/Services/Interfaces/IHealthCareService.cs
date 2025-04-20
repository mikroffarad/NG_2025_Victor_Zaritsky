using HealthCareBL.Models;

namespace HealthCareBL.Services.Interfaces
{
    public interface IHealthCareService
    {
        Task<IEnumerable<HealthCareDto>> GetAllHealthCareRecords();
        Task<HealthCareDto> GetHealthCareById(Guid id);
        Task<Guid> AddHealthCareRecord(HealthCareDto healthCareDto);
        Task<Guid> UpdateHealthCareRecord(Guid id, HealthCareDto healthCareDto);
        Task DeleteHealthCareRecord(Guid id);
        Task<IEnumerable<HealthCareDto>> GetHealthCareRecordsByPet(Guid petId);
        Task<IEnumerable<HealthCareDto>> GetHealthCareRecordsByVendor(Guid vendorId);
        Task<IEnumerable<HealthCareDto>> GetExpiringHealthCareRecords();
    }
}