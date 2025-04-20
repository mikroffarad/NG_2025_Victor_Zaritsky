using DAL_Core.Enums;
using SentinelBusinessLayer.Clients;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace SentinelBusinessLayer.Service;

public class PetService : IPetService
{
    private readonly IPetStoreClient _petStoreClient;

    public PetService(IPetStoreClient petStoreClient)
    {
        _petStoreClient = petStoreClient;
    }

    public async Task<IEnumerable<PetDto>?> GetAllPets()
    {
        try
        {
            var response = await _petStoreClient.GetAllPets();
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                return response.Content;
            }

            return null;
        }
        catch (ApiException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<PetDto?> GetPetById(Guid id)
    {
        try
        {
            var response = await _petStoreClient.GetPetById(id);
            return response.IsSuccessStatusCode ? response.Content : null;
        }
        catch { return null; }
    }

    public async Task<PetDto?> AddPet(PetDto petDto)
    {
        try
        {
            var response = await _petStoreClient.AddPet(petDto);
            return response.IsSuccessStatusCode ? response.Content : null;
        }
        catch { return null; }
    }

    public async Task<bool> UpdatePet(Guid id, PetDto petDto)
    {
        try
        {
            var response = await _petStoreClient.UpdatePet(id, petDto);
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<IEnumerable<PetDto>?> GetPetsByStore(Guid storeId)
    {
        try
        {
            var response = await _petStoreClient.GetPetsByStore(storeId);
            return response.IsSuccessStatusCode ? response.Content : null;
        }
        catch { return null; }
    }

    public async Task<IEnumerable<PetDto>?> GetPetsByType(PetTypes type)
    {
        try
        {
            var response = await _petStoreClient.GetPetsByType(type);
            return response.IsSuccessStatusCode ? response.Content : null;
        }
        catch { return null; }
    }

    public async Task<bool> AdoptPet(AdoptionRequest request)
    {
        try
        {
            var response = await _petStoreClient.AdoptPet(request);
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }
}