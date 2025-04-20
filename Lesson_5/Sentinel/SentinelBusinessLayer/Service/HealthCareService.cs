using Microsoft.Extensions.Logging;
using Refit;
using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service;

public class HealthCareService : IHealthCareService
{
    private readonly IHealthCareClient _healthCareClient;
    private readonly ILogger<HealthCareService> _logger;

    public HealthCareService(IHealthCareClient healthCareClient, ILogger<HealthCareService> logger)
    {
        _healthCareClient = healthCareClient;
        _logger = logger;
    }

    public async Task<IEnumerable<HealthCareDto>?> GetAllHealthCareRecords()
    {
        try
        {
            var response = await _healthCareClient.GetAllHealthCareRecords();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get all healthcare records. Status: {StatusCode}, Error: {Error}", response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting all healthcare records. Status: {StatusCode}", ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting all healthcare records.");
            return null;
        }
    }

    public async Task<HealthCareDto?> GetHealthCareById(Guid id)
    {
        try
        {
            var response = await _healthCareClient.GetHealthCareById(id);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogWarning("Healthcare record {Id} not found or failed to retrieve. Status: {StatusCode}", id, response.StatusCode);
            return null;
        }
        catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Healthcare record {Id} not found via API. Status: {StatusCode}", id, ex.StatusCode);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting healthcare record {Id}. Status: {StatusCode}", id, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting healthcare record {Id}.", id);
            return null;
        }
    }

    public async Task<bool> AddHealthCareRecord(HealthCareDto healthCareDto)
    {
        try
        {
            var response = await _healthCareClient.AddHealthCareRecord(healthCareDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to add healthcare record. Status: {StatusCode}, Error: {Error}", response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while adding healthcare record. Status: {StatusCode}", ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while adding healthcare record.");
            return false;
        }
    }

    public async Task<bool> UpdateHealthCareRecord(Guid id, HealthCareDto healthCareDto)
    {
        try
        {
            var response = await _healthCareClient.UpdateHealthCareRecord(id, healthCareDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to update healthcare record {Id}. Status: {StatusCode}, Error: {Error}", id, response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while updating healthcare record {Id}. Status: {StatusCode}", id, ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while updating healthcare record {Id}.", id);
            return false;
        }
    }

    public async Task<bool> DeleteHealthCareRecord(Guid id)
    {
        try
        {
            var response = await _healthCareClient.DeleteHealthCareRecord(id);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to delete healthcare record {Id}. Status: {StatusCode}, Error: {Error}", id, response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while deleting healthcare record {Id}. Status: {StatusCode}", id, ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while deleting healthcare record {Id}.", id);
            return false;
        }
    }

    public async Task<IEnumerable<HealthCareDto>?> GetHealthCareRecordsByPet(Guid petId)
    {
        try
        {
            var response = await _healthCareClient.GetHealthCareRecordsByPet(petId);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get healthcare records for pet {PetId}. Status: {StatusCode}, Error: {Error}", petId, response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting healthcare records for pet {PetId}. Status: {StatusCode}", petId, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting healthcare records for pet {PetId}.", petId);
            return null;
        }
    }

    public async Task<IEnumerable<HealthCareDto>?> GetHealthCareRecordsByVendor(Guid vendorId)
    {
        try
        {
            var response = await _healthCareClient.GetHealthCareRecordsByVendor(vendorId);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get healthcare records for vendor {VendorId}. Status: {StatusCode}, Error: {Error}", vendorId, response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting healthcare records for vendor {VendorId}. Status: {StatusCode}", vendorId, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting healthcare records for vendor {VendorId}.", vendorId);
            return null;
        }
    }

    public async Task<IEnumerable<HealthCareDto>?> GetExpiringHealthCareRecords()
    {
        try
        {
            var response = await _healthCareClient.GetExpiringHealthCareRecords();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get expiring healthcare records. Status: {StatusCode}, Error: {Error}", response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting expiring healthcare records. Status: {StatusCode}", ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting expiring healthcare records.");
            return null;
        }
    }
}