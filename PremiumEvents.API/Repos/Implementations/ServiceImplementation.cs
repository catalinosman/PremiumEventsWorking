using Microsoft.EntityFrameworkCore;
using PremiumEvents.API.Data;
using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos.Implementations
{
    public class ServiceImplementation : ServiceInterface
    {
        private readonly PremiumEventsDbContext _dbContext;

        public ServiceImplementation(PremiumEventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Service> CreateAsync(Service service)
        {
            await _dbContext.Services.AddAsync(service);
            await _dbContext.SaveChangesAsync();
            return service;
        }

        public async Task<Service?> DeleteAsync(Guid id)
        {
            var service = await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return null;
            }

            _dbContext.Remove(service);
            await _dbContext.SaveChangesAsync(true);
            return service;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _dbContext.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Service?> UpdateAsync(Guid id, Service service)
        {
            var serviceToUpdate = await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == id);

            if (serviceToUpdate == null)
            {
                return null;
            }

            serviceToUpdate.Name = service.Name;
            serviceToUpdate.Description = service.Description;
            serviceToUpdate.Capacity = service.Capacity;
           // serviceToUpdate.PhoneNumber = service.PhoneNumber;

            await _dbContext.SaveChangesAsync();

            return serviceToUpdate;
        }
    }
}
