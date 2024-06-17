using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Models.DTOs.CityDtos
{
    public class AddCityDto
    {
        public string Name { get; set; }
        public Guid CountyId { get; set; }
    }
}
