using DAL_Core.Enums;
using Refit;
using SentinelBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Clients;

public interface IPetStoreClient
{
    // Pets Controller
    [Get("/api/pets")]
    Task<ApiResponse<IEnumerable<PetDto>>> GetAllPets();

    [Get("/api/pets/{id}")]
    Task<ApiResponse<PetDto>> GetPetById(Guid id);

    [Post("/api/pets")]
    Task<ApiResponse<PetDto>> AddPet([Body] PetDto petDto);

    [Put("/api/pets/{id}")]
    Task<IApiResponse> UpdatePet(Guid id, [Body] PetDto petDto);

    [Get("/api/pets/store/{storeId}")]
    Task<ApiResponse<IEnumerable<PetDto>>> GetPetsByStore(Guid storeId);

    [Get("/api/pets/type/{type}")]
    Task<ApiResponse<IEnumerable<PetDto>>> GetPetsByType(PetTypes type);

    [Post("/api/pets/adopt")]
    Task<IApiResponse> AdoptPet([Body] AdoptionRequest request);

    // Stores Controller
    [Get("/api/stores")]
    Task<ApiResponse<IEnumerable<StoreDto>>> GetAllStores();

    [Get("/api/stores/{id}")]
    Task<ApiResponse<StoreDto>> GetStoreById(Guid id);

    [Put("/api/stores/{id}")]
    Task<IApiResponse> UpdateStore(Guid id, [Body] StoreDto storeDto);

    [Get("/api/stores/{storeId}/pets")]
    Task<ApiResponse<IEnumerable<PetDto>>> GetStorePets(Guid storeId);

    [Get("/api/stores/{storeId}/healthcare")]
    Task<ApiResponse<IEnumerable<object>>> GetStoreHealthcareRecords(Guid storeId);
}