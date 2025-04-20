using AutoMapper;
using DAL_Core.Entities;
using PetStoreBL.Models;
using PetStoreBL.Services.Interfaces;
using PetStoreDal.Repositories.Interfaces;

namespace PetStoreBL.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepository, IPetRepository petRepository, IMapper mapper)
        {
            _storeRepository = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            _petRepository = petRepository ?? throw new ArgumentNullException(nameof(petRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<StoreDto>> GetAllStores()
        {
            var stores = await _storeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StoreDto>>(stores);
        }

        public async Task<StoreDto> GetStoreById(Guid id)
        {
            var store = await _storeRepository.GetStoreWithDetailsAsync(id);
            if (store == null)
            {
                throw new Exception($"Store with ID={id} not found");
            }
            return _mapper.Map<StoreDto>(store);
        }

        public async Task<Guid> UpdateStore(Guid id, StoreDto storeDto)
        {
            var existingStore = await _storeRepository.GetAsync(id);
            if (existingStore == null)
            {
                throw new Exception($"Store with ID={id} not found");
            }

            existingStore.Name = storeDto.Name;
            existingStore.Description = storeDto.Description;
            existingStore.Address = storeDto.Address;
            existingStore.City = storeDto.City;

            return await _storeRepository.UpdateAsync(existingStore);
        }

        public async Task<IEnumerable<PetDto>> GetStorePets(Guid storeId)
        {
            var pets = await _storeRepository.GetStorePetsAsync(storeId);
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<IEnumerable<object>> GetStoreHealthcareRecords(Guid storeId)
        {
            return new List<object>();
        }
    }
}