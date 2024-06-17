using PremiumEvents.API.Models.DTOs.ServiceCategoryDtos;
using PremiumEvents.API.Models.DTOs.ServiceDtos;

namespace PremiumEvents.API.Models.DTOs.CityDtos
{
    public class CityContainsDto
    {
        public string Name { get; set; }
        public List<CityServiceDto> Services { get; set; }
        public List<CategoryCityDto> ServiceCategories { get; set; }
    }
}
