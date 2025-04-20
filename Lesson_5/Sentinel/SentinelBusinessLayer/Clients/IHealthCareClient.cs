using DAL_Core.Enums;
using Refit;
using SentinelBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Clients;

public interface IHealthCareClient
{
    // Healthcare Controller
    [Get("/api/healthcare")]
    Task<ApiResponse<IEnumerable<HealthCareDto>>> GetAllHealthCareRecords();

    [Get("/api/healthcare/{id}")]
    Task<ApiResponse<HealthCareDto>> GetHealthCareById(Guid id);

    [Post("/api/healthcare")]
    Task<IApiResponse> AddHealthCareRecord([Body] HealthCareDto healthCareDto);

    [Put("/api/healthcare/{id}")]
    Task<IApiResponse> UpdateHealthCareRecord(Guid id, [Body] HealthCareDto healthCareDto);

    [Delete("/api/healthcare/{id}")]
    Task<IApiResponse> DeleteHealthCareRecord(Guid id);

    [Get("/api/healthcare/pet/{petId}")]
    Task<ApiResponse<IEnumerable<HealthCareDto>>> GetHealthCareRecordsByPet(Guid petId);

    [Get("/api/healthcare/vendor/{vendorId}")]
    Task<ApiResponse<IEnumerable<HealthCareDto>>> GetHealthCareRecordsByVendor(Guid vendorId);

    [Get("/api/healthcare/expiring")]
    Task<ApiResponse<IEnumerable<HealthCareDto>>> GetExpiringHealthCareRecords();

    // Vendors Controller
    [Get("/api/vendors")]
    Task<ApiResponse<IEnumerable<VendorDto>>> GetAllVendors();

    [Get("/api/vendors/{id}")]
    Task<ApiResponse<VendorDto>> GetVendorById(Guid id);

    [Post("/api/vendors")]
    Task<IApiResponse> AddVendor([Body] VendorDto vendorDto);

    [Put("/api/vendors/{id}")]
    Task<IApiResponse> UpdateVendor(Guid id, [Body] VendorDto vendorDto);

    [Delete("/api/vendors/{id}")]
    Task<IApiResponse> DeleteVendor(Guid id);

    [Get("/api/vendors/contract/{type}")]
    Task<ApiResponse<IEnumerable<VendorDto>>> GetVendorsByContractType(ContractType type);

    [Get("/api/vendors/{vendorId}/healthcare")]
    Task<ApiResponse<IEnumerable<HealthCareDto>>> GetVendorHealthCareRecords(Guid vendorId);
}