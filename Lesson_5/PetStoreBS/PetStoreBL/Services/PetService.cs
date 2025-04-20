using AutoMapper;
using DAL_Core.Entities;
using DAL_Core.Enums;
using PetStoreBL.Models;
using PetStoreBL.Services.Interfaces;
using PetStoreDal.Repositories.Interfaces;

namespace PetStoreBL.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepository, IStoreRepository storeRepository, IMapper mapper)
        {
            _petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PetDto>> GetAllPets()
        {
            var pets = await _petRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto> GetPetById(Guid id)
        {
            var pet = await _petRepository.GetPetWithDetailsAsync(id);
            if (pet == null)
            {
                throw new Exception($"Pet with ID={id} not found");
            }
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<Guid> AddPet(PetDto petDto)
        {
            var pet = _mapper.Map<Pet>(petDto);

            // Verify store exists
            var store = await _storeRepository.GetAsync(pet.StoreId);
            if (store == null)
            {
                throw new Exception($"Store with ID={pet.StoreId} not found");
            }

            return await _petRepository.CreateAsync(pet);
        }

        public async Task<Guid> UpdatePet(Guid id, PetDto petDto)
        {
            var existingPet = await _petRepository.GetAsync(id);
            if (existingPet == null)
            {
                throw new Exception($"Pet with ID={id} not found");
            }

            if (existingPet.StoreId != petDto.StoreId)
            {
                var store = await _storeRepository.GetAsync(petDto.StoreId);
                if (store == null)
                {
                    throw new Exception($"Store with ID={petDto.StoreId} not found");
                }
            }

            existingPet.Name = petDto.Name;
            existingPet.Breed = petDto.Breed;
            existingPet.Type = petDto.Type;
            existingPet.StoreId = petDto.StoreId;

            return await _petRepository.UpdateAsync(existingPet);
        }

        public async Task<IEnumerable<PetDto>> GetPetsByStore(Guid storeId)
        {
            var pets = await _petRepository.GetPetsByStoreAsync(storeId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<IEnumerable<PetDto>> GetPetsByType(PetTypes type)
        {
            var pets = await _petRepository.GetPetsByTypeAsync(type);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task AdoptPet(Guid petId, Guid customerId)
        {

            var pet = await _petRepository.GetAsync(petId);
            if (pet == null)
            {
                throw new Exception($"Pet with ID={petId} not found");
            }

            Console.WriteLine($"Pet {petId} adopted by customer {customerId}");
        }
    }
}