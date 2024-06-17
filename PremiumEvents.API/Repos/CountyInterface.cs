using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos
{
    public interface CountyInterface
    {
        public Task<List<County>> GetAllAsync();
        public Task<County?> GetByIdAsync(Guid id);
        public Task<County> CreateAsync(County county);
        public Task<County?> UpdateAsync(Guid id, County county);
        public Task<County?> DeleteAsync(Guid id);
    }
}
