using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos
{
    public interface CityInterface
    {
        public Task<List<City>> GetAllAsync();
        public Task<City?> GetByIdAsync(Guid id);
        public Task<City> CreateAsync(City city);
        public Task<City?> UpdateAsync(Guid id, City city);
        public Task<City?> DeleteAsync(Guid id);
    }
}
