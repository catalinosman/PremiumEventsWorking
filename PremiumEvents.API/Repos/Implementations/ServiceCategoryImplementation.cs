using Microsoft.EntityFrameworkCore;
using PremiumEvents.API.Data;
using PremiumEvents.API.Models.Domain;
using System.Diagnostics.Metrics;

namespace PremiumEvents.API.Repos.Implementations
{
    public class ServiceCategoryImplementation : ServiceCategoryInterface
    {
        private readonly PremiumEventsDbContext _dbContext;

        public ServiceCategoryImplementation(PremiumEventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ServiceCategory> CreateAsync(ServiceCategory serviceCategory)
        {
            await _dbContext.ServiceCategories.AddAsync(serviceCategory);
            await _dbContext.SaveChangesAsync();
            return serviceCategory;
        }

        public async Task<ServiceCategory?> DeleteAsync(Guid id)
        {
            var categoryExists = await _dbContext.ServiceCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (categoryExists == null)
            {
                return null;
            }

            _dbContext.Remove(categoryExists);
            await _dbContext.SaveChangesAsync();
            return categoryExists;
        }

        public async Task<List<ServiceCategory>> GetAllAsync()
        {
            return await _dbContext.ServiceCategories.Include(s => s.Services)
                                                     .ToListAsync();
        }

        public async Task<ServiceCategory?> GetByIdAsync(Guid id)
        {
            return await _dbContext.ServiceCategories.Include(s => s.Services)
                                                     .FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<ServiceCategory?> UpdateAsync(Guid id, ServiceCategory serviceCategory)
        {
            var getCategory = await _dbContext.ServiceCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (getCategory == null)
            {
                return null;
            }

            getCategory.Name = serviceCategory.Name;
            getCategory.ImageUrl = serviceCategory.ImageUrl;
            await _dbContext.SaveChangesAsync();

            return getCategory;
        }
    }
}
