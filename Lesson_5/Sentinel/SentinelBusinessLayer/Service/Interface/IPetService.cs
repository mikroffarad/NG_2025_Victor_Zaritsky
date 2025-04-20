using DAL_Core.Enums;
using SentinelBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentinelBusinessLayer.Service.Interface;

public interface IPetService
{
    Task<IEnumerable<PetDto>?> GetAllPets();
    Task<PetDto?> GetPetById(Guid id);
    Task<PetDto?> AddPet(PetDto petDto);
    Task<bool> UpdatePet(Guid id, PetDto petDto);
    Task<IEnumerable<PetDto>?> GetPetsByStore(Guid storeId);
    Task<IEnumerable<PetDto>?> GetPetsByType(PetTypes type);
    Task<bool> AdoptPet(AdoptionRequest request);
}