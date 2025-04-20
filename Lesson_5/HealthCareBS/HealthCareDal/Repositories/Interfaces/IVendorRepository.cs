using System.Numerics;
using DAL_Core.Entities;
using DAL_Core.Enums;

namespace HealthCareDal.Repositories.Interfaces
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        Task<Vendor?> GetVendorWithDetailsAsync(Guid id);
        Task<ICollection<Vendor>> GetVendorsByContractTypeAsync(ContractType type);
    }
}