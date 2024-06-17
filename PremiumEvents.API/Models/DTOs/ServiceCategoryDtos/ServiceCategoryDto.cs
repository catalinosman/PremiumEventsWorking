namespace PremiumEvents.API.Models.DTOs.ServiceCategoryDtos
{
    public class ServiceCategoryDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<string> ServiceNames { get; set; }
    }
}
