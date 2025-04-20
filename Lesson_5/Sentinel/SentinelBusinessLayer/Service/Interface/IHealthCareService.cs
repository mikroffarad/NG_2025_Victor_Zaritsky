using SentinelBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service.Interface;

public interface IHealthCareService
{
    Task<IEnumerable<HealthCareDto>?> GetAllHealthCareRecords();
    Task<HealthCareDto?> GetHealthCareById(Guid id);
    Task<bool> AddHealthCareRecord(HealthCareDto healthCareDto);
    Task<bool> UpdateHealthCareRecord(Guid id, HealthCareDto healthCareDto);
    Task<bool> DeleteHealthCareRecord(Guid id);
    Task<IEnumerable<HealthCareDto>?> GetHealthCareRecordsByPet(Guid petId);
    Task<IEnumerable<HealthCareDto>?> GetHealthCareRecordsByVendor(Guid vendorId);
    Task<IEnumerable<HealthCareDto>?> GetExpiringHealthCareRecords();
}