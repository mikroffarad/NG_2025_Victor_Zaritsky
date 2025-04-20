using Microsoft.Extensions.Logging;
using Refit;
using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service;

public class StoreService : IStoreService
{
    private readonly IPetStoreClient _petStoreClient;
    private readonly ILogger<StoreService> _logger;

    public StoreService(IPetStoreClient petStoreClient, ILogger<StoreService> logger)
    {
        _petStoreClient = petStoreClient;
        _logger = logger;
    }

    public async Task<IEnumerable<StoreDto>?> GetAllStores()
    {
        try
        {
            var response = await _petStoreClient.GetAllStores();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get stores from PetStore service. Status: {StatusCode}, Error: {Error}", response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting all stores. Status: {StatusCode}, Content: {Content}", ex.StatusCode, ex.Content);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting all stores.");
            return null;
        }
    }

    public async Task<StoreDto?> GetStoreById(Guid id)
    {
        try
        {
            var response = await _petStoreClient.GetStoreById(id);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogWarning("Store with ID {StoreId} not found or failed to retrieve. Status: {StatusCode}", id, response.StatusCode);
            return null;
        }
        catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Store with ID {StoreId} not found via API. Status: {StatusCode}", id, ex.StatusCode);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting store by ID {StoreId}. Status: {StatusCode}", id, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting store by ID {StoreId}.", id);
            return null;
        }
    }

    public async Task<bool> UpdateStore(Guid id, StoreDto storeDto)
    {
        try
        {
            var response = await _petStoreClient.UpdateStore(id, storeDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to update store {StoreId}. Status: {StatusCode}, Error: {Error}", id, response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while updating store {StoreId}. Status: {StatusCode}", id, ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while updating store {StoreId}.", id);
            return false;
        }
    }

    public async Task<IEnumerable<PetDto>?> GetStorePets(Guid storeId)
    {
        try
        {
            var response = await _petStoreClient.GetStorePets(storeId);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get pets for store {StoreId}. Status: {StatusCode}, Error: {Error}", storeId, response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting pets for store {StoreId}. Status: {StatusCode}", storeId, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting pets for store {StoreId}.", storeId);
            return null;
        }
    }

    public async Task<IEnumerable<object>?> GetStoreHealthcareRecords(Guid storeId)
    {
        _logger.LogInformation("Attempting to get healthcare records for store {StoreId}.", storeId);

        await Task.Delay(10);
        _logger.LogWarning("GetStoreHealthcareRecords for store {StoreId} is not fully implemented in Sentinel. Returning empty list.", storeId);
        return new List<object>();
    }
}