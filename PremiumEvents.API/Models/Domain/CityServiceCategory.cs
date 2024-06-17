namespace PremiumEvents.API.Models.Domain
{
    public class CityServiceCategory
    {
        public Guid CityId { get; set; }
        public City City { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
    }
}
