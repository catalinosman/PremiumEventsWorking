namespace PremiumEvents.API.Models.Domain
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<CityServiceCategory> CityServiceCategories { get; set; }
    }
}
