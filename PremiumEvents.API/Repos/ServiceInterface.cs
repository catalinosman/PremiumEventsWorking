using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos
{
    public interface ServiceInterface
    {
        public Task<List<Service>> GetAllAsync();
        public Task<Service?> GetByIdAsync(Guid id);
        public Task<Service> CreateAsync(Service service);
        public Task<Service?> UpdateAsync(Guid id, Service service);
        public Task<Service?> DeleteAsync(Guid id);
    }
}
