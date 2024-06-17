namespace PremiumEvents.API.Models.Domain
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountyId { get; set; }
        public County County { get; set; }
        public ICollection<CityService> CityServices { get; set; }
        public ICollection<CityServiceCategory> CityServiceCategories { get; set; }
    }
}