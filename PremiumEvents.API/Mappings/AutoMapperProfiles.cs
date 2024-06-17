using AutoMapper;
using PremiumEvents.API.Models.Domain;
using PremiumEvents.API.Models.DTOs.CityDtos;
using PremiumEvents.API.Models.DTOs.CountyDtos;
using PremiumEvents.API.Models.DTOs.ServiceCategoryDtos;
using PremiumEvents.API.Models.DTOs.ServiceDtos;

namespace PremiumEvents.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<County, UpdateCountyDto>().ReverseMap();
            CreateMap<UpdateCountyDto, RequestCountyDto>().ReverseMap();
            CreateMap<County, RequestCountyDto>().ReverseMap();
            CreateMap<County, CountyDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<City, CityContainsDto>().ReverseMap();
            CreateMap<City, CityContainsDto>()
                    .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.CityServices.Select(cs => cs.Service)))
                    .ForMember(dest => dest.ServiceCategories, opt => opt.MapFrom(src => src.CityServiceCategories.Select(csc => csc.ServiceCategory)));
            CreateMap<AddCityDto, CityDto>().ReverseMap();
            CreateMap<UpdateCityDto, CityDto>().ReverseMap();
            CreateMap<AddCityDto, City>().ReverseMap();
            CreateMap<City, RequestCityDto>().ReverseMap();
            CreateMap<City, UpdateCityDto>().ReverseMap();
            CreateMap<County, CountyDto>()
                    .ForMember(dest => dest.CityNames, opt => opt.MapFrom(src => src.City.Select(c => c.Name)));
            CreateMap<ServiceCategory, ServiceCategoryDto>()
                    .ForMember(dest => dest.ServiceNames, opt => opt.MapFrom(src => src.Services.Select(c => c.Name)));
            CreateMap<ServiceCategory, AddServiceCategoryDto>().ReverseMap();
            CreateMap<ServiceCategory, DisplayServiceCategoryDto>().ReverseMap();
            CreateMap<ServiceCategory, UpdateServiceCategoryDto>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<ServiceDto, AddServiceDto>().ReverseMap();
            CreateMap<Service, AddServiceDto>().ReverseMap();
            CreateMap<Service, CityServiceDto>().ReverseMap();
            CreateMap<ServiceCategory, CategoryCityDto>().ReverseMap();
            CreateMap<UpdatedServiceDto, Service>().ReverseMap();
        }
    }
}
