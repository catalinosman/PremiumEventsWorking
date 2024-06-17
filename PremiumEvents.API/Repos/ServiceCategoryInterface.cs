using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos
{
    public interface ServiceCategoryInterface
    {
        public Task<List<ServiceCategory>> GetAllAsync();
        public Task<ServiceCategory?> GetByIdAsync(Guid id);
        public Task<ServiceCategory> CreateAsync(ServiceCategory serviceCategory);
        public Task<ServiceCategory?> UpdateAsync(Guid id, ServiceCategory serviceCategory);
        public Task<ServiceCategory?> DeleteAsync(Guid id);
    }
}
