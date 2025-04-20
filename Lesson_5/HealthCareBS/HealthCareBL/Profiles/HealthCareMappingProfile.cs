using AutoMapper;
using DAL_Core.Entities;
using HealthCareBL.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthCareBL.Profiles
{
    public class HealthCareMappingProfile : Profile
    {
        public HealthCareMappingProfile()
        {
            CreateMap<HealthCare, HealthCareDto>()
                .ForMember(dest => dest.IsExpired,
                           opt => opt.MapFrom(src => src.ExpirationDate <= DateTime.Now))
                .ReverseMap();

            CreateMap<Vendor, VendorDto>().ReverseMap();
        }
    }
}