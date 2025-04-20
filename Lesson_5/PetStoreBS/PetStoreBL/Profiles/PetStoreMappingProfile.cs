using AutoMapper;
using DAL_Core.Entities;
using PetStoreBL.Models;

namespace PetStoreBL.Profiles
{
    public class PetStoreMappingProfile : Profile
    {
        public PetStoreMappingProfile()
        {
            CreateMap<Pet, PetDto>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.Name : string.Empty));

            CreateMap<PetDto, Pet>()
                .ForMember(dest => dest.Store, opt => opt.Ignore())
                .ForMember(dest => dest.HealthCares, opt => opt.Ignore());

            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.PetCount, opt => opt.MapFrom(src => src.Pets != null ? src.Pets.Count : 0));

            CreateMap<StoreDto, Store>()
                .ForMember(dest => dest.Pets, opt => opt.Ignore());
        }
    }
}