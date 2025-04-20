using DAL_Core.Enums;
using SentinelBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service.Interface;

public interface IVendorService
{
    Task<IEnumerable<VendorDto>?> GetAllVendors();
    Task<VendorDto?> GetVendorById(Guid id);
    Task<bool> AddVendor(VendorDto vendorDto);
    Task<bool> UpdateVendor(Guid id, VendorDto vendorDto);
    Task<bool> DeleteVendor(Guid id);
    Task<IEnumerable<VendorDto>?> GetVendorsByContractType(ContractType type);
    Task<IEnumerable<HealthCareDto>?> GetVendorHealthCareRecords(Guid vendorId);
}