using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Models.DTOs.CountyDtos
{
    public class CountyDto
    {
        public string Name { get; set; }
        public ICollection<string> CityNames { get; set; }
    }
}
