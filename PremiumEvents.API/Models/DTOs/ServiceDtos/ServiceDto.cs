namespace PremiumEvents.API.Models.DTOs.ServiceDtos
{
    public class ServiceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoPresentation { get; set; }
        public string? Genre { get; set; }
        public string? Drinks { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? Capacity { get; set; }
    }
}
