using DAL_Core.Enums;
using HealthCareBL.Models;

namespace HealthCareBL.Services.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDto>> GetAllVendors();
        Task<VendorDto> GetVendorById(Guid id);
        Task<Guid> AddVendor(VendorDto vendorDto);
        Task<Guid> UpdateVendor(Guid id, VendorDto vendorDto);
        Task DeleteVendor(Guid id);
        Task<IEnumerable<VendorDto>> GetVendorsByContractType(ContractType type);
        Task<IEnumerable<HealthCareDto>> GetVendorHealthCareRecords(Guid vendorId);
    }
}