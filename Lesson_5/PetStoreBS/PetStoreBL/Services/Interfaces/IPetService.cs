using DAL_Core.Enums;
using PetStoreBL.Models;

namespace PetStoreBL.Services.Interfaces
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllPets();
        Task<PetDto> GetPetById(Guid id);
        Task<Guid> AddPet(PetDto petDto);
        Task<Guid> UpdatePet(Guid id, PetDto petDto);
        Task<IEnumerable<PetDto>> GetPetsByStore(Guid storeId);
        Task<IEnumerable<PetDto>> GetPetsByType(PetTypes type);
        Task AdoptPet(Guid petId, Guid customerId);
    }
}