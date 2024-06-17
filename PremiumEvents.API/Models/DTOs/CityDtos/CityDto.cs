using PremiumEvents.API.Models.Domain;
using PremiumEvents.API.Models.DTOs.CountyDtos;

namespace PremiumEvents.API.Models.DTOs.CityDtos
{
    public class CityDto
    {
        public string Name { get; set; }
        public Guid CountyId { get; set; }
        public ICollection<string> ServiceNames { get; set; }
        public ICollection<string> CategoryNames { get; set; }
    }
}
