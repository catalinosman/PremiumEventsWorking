namespace PremiumEvents.API.Models.Domain
{
    public class CityService
    {
        public Guid CityId { get; set; }
        public City City { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
