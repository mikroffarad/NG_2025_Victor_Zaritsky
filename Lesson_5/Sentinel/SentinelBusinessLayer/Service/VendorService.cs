using DAL_Core.Enums;
using Microsoft.Extensions.Logging;
using Refit;
using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service;

public class VendorService : IVendorService
{
    private readonly IHealthCareClient _healthCareClient;
    private readonly ILogger<VendorService> _logger;

    public VendorService(IHealthCareClient healthCareClient, ILogger<VendorService> logger)
    {
        _healthCareClient = healthCareClient;
        _logger = logger;
    }

    public async Task<IEnumerable<VendorDto>?> GetAllVendors()
    {
        try
        {
            var response = await _healthCareClient.GetAllVendors();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get all vendors. Status: {StatusCode}, Error: {Error}", response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting all vendors. Status: {StatusCode}", ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting all vendors.");
            return null;
        }
    }

    public async Task<VendorDto?> GetVendorById(Guid id)
    {
        try
        {
            var response = await _healthCareClient.GetVendorById(id);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogWarning("Vendor {Id} not found or failed to retrieve. Status: {StatusCode}", id, response.StatusCode);
            return null;
        }
        catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Vendor {Id} not found via API. Status: {StatusCode}", id, ex.StatusCode);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting vendor {Id}. Status: {StatusCode}", id, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting vendor {Id}.", id);
            return null;
        }
    }

    public async Task<bool> AddVendor(VendorDto vendorDto)
    {
        try
        {
            var response = await _healthCareClient.AddVendor(vendorDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to add vendor. Status: {StatusCode}, Error: {Error}", response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while adding vendor. Status: {StatusCode}", ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while adding vendor.");
            return false;
        }
    }

    public async Task<bool> UpdateVendor(Guid id, VendorDto vendorDto)
    {
        try
        {
            var response = await _healthCareClient.UpdateVendor(id, vendorDto);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to update vendor {Id}. Status: {StatusCode}, Error: {Error}", id, response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while updating vendor {Id}. Status: {StatusCode}", id, ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while updating vendor {Id}.", id);
            return false;
        }
    }

    public async Task<bool> DeleteVendor(Guid id)
    {
        try
        {
            var response = await _healthCareClient.DeleteVendor(id);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to delete vendor {Id}. Status: {StatusCode}, Error: {Error}", id, response.StatusCode, response.Error?.Message);
            }
            return response.IsSuccessStatusCode;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while deleting vendor {Id}. Status: {StatusCode}", id, ex.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while deleting vendor {Id}.", id);
            return false;
        }
    }

    public async Task<IEnumerable<VendorDto>?> GetVendorsByContractType(ContractType type)
    {
        try
        {
            var response = await _healthCareClient.GetVendorsByContractType(type);
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }
            _logger.LogError("Failed to get vendors by type {Type}. Status: {StatusCode}, Error: {Error}", type, response.StatusCode, response.Error?.Message);
            return null;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API exception while getting vendors by type {Type}. Status: {StatusCode}", type, ex.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while getting vendors by type {Type}.", type);
            return null;
        }
    }

    public async Task<IEnumerable<HealthCareDto>?> GetVendorHealthCareRecords(Guid vendorId)
    {
        try
        {
            var response = await _healthCareClient.GetVendorHealthCareRecords(vendorId);
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
}