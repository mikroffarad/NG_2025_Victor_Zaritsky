using AutoMapper;
using DAL_Core.Entities;
using HealthCareBL.Models;
using HealthCareBL.Services.Interfaces;
using HealthCareDal.Repositories.Interfaces;

namespace HealthCareBL.Services
{
    public class HealthCareService : IHealthCareService
    {
        private readonly IHealthCareRepository _healthCareRepository;
        private readonly IMapper _mapper;

        public HealthCareService(IHealthCareRepository healthCareRepository, IMapper mapper)
        {
            _healthCareRepository = healthCareRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HealthCareDto>> GetAllHealthCareRecords()
        {
            var records = await _healthCareRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HealthCareDto>>(records);
        }

        public async Task<HealthCareDto> GetHealthCareById(Guid id)
        {
            var record = await _healthCareRepository.GetHealthCareWithDetailsAsync(id);
            if (record == null) throw new Exception($"Health care record {id} not found");
            return _mapper.Map<HealthCareDto>(record);
        }

        public async Task<Guid> AddHealthCareRecord(HealthCareDto healthCareDto)
        {
            var entity = _mapper.Map<HealthCare>(healthCareDto);
            return await _healthCareRepository.CreateAsync(entity);
        }

        public async Task<Guid> UpdateHealthCareRecord(Guid id, HealthCareDto healthCareDto)
        {
            var existing = await _healthCareRepository.GetAsync(id);
            if (existing == null) throw new Exception($"Health care record {id} not found");

            _mapper.Map(healthCareDto, existing);
            return await _healthCareRepository.UpdateAsync(existing);
        }

        public async Task DeleteHealthCareRecord(Guid id)
        {
            await _healthCareRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<HealthCareDto>> GetHealthCareRecordsByPet(Guid petId)
        {
            var records = await _healthCareRepository.GetHealthCaresByPetAsync(petId);
            return _mapper.Map<IEnumerable<HealthCareDto>>(records);
        }

        public async Task<IEnumerable<HealthCareDto>> GetHealthCareRecordsByVendor(Guid vendorId)
        {
            var records = await _healthCareRepository.GetHealthCaresByVendorAsync(vendorId);
            return _mapper.Map<IEnumerable<HealthCareDto>>(records);
        }

        public async Task<IEnumerable<HealthCareDto>> GetExpiringHealthCareRecords()
        {
            var records = await _healthCareRepository.GetExpiringHealthCaresAsync();
            return _mapper.Map<IEnumerable<HealthCareDto>>(records);
        }
    }
}