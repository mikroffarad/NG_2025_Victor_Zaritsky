using SentinelBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service.Interface;

public interface IStoreService
{
    Task<IEnumerable<StoreDto>?> GetAllStores();
    Task<StoreDto?> GetStoreById(Guid id);
    Task<bool> UpdateStore(Guid id, StoreDto storeDto);
    Task<IEnumerable<PetDto>?> GetStorePets(Guid storeId);
    Task<IEnumerable<object>?> GetStoreHealthcareRecords(Guid storeId);
}