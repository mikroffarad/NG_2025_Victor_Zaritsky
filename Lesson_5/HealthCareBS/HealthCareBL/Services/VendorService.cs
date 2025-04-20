using AutoMapper;
using DAL_Core.Entities;
using DAL_Core.Enums;
using HealthCareBL.Models;
using HealthCareBL.Services.Interfaces;
using HealthCareDal.Repositories.Interfaces;

namespace HealthCareBL.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IHealthCareRepository _healthCareRepository;
        private readonly IMapper _mapper;

        public VendorService(
            IVendorRepository vendorRepository,
            IHealthCareRepository healthCareRepository,
            IMapper mapper)
        {
            _vendorRepository = vendorRepository;
            _healthCareRepository = healthCareRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendorDto>> GetAllVendors()
        {
            var vendors = await _vendorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<VendorDto>>(vendors);
        }

        public async Task<VendorDto> GetVendorById(Guid id)
        {
            var vendor = await _vendorRepository.GetVendorWithDetailsAsync(id);
            if (vendor == null) throw new Exception($"Vendor {id} not found");
            return _mapper.Map<VendorDto>(vendor);
        }

        public async Task<Guid> AddVendor(VendorDto vendorDto)
        {
            var entity = _mapper.Map<Vendor>(vendorDto);
            return await _vendorRepository.CreateAsync(entity);
        }

        public async Task<Guid> UpdateVendor(Guid id, VendorDto vendorDto)
        {
            var existing = await _vendorRepository.GetAsync(id);
            if (existing == null) throw new Exception($"Vendor {id} not found");

            _mapper.Map(vendorDto, existing);
            return await _vendorRepository.UpdateAsync(existing);
        }

        public async Task DeleteVendor(Guid id)
        {
            await _vendorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VendorDto>> GetVendorsByContractType(ContractType type)
        {
            var vendors = await _vendorRepository.GetVendorsByContractTypeAsync(type);
            return _mapper.Map<IEnumerable<VendorDto>>(vendors);
        }

        public async Task<IEnumerable<HealthCareDto>> GetVendorHealthCareRecords(Guid vendorId)
        {
            var records = await _healthCareRepository.GetHealthCaresByVendorAsync(vendorId);
            return _mapper.Map<IEnumerable<HealthCareDto>>(records);
        }
    }
}