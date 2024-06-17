using Microsoft.EntityFrameworkCore;
using PremiumEvents.API.Data;
using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos.Implementations
{
    public class CountyImplementation : CountyInterface
    {
        private readonly PremiumEventsDbContext _dbContext;

        public CountyImplementation(PremiumEventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<County> CreateAsync(County county)
        {
            await _dbContext.Counties.AddAsync(county);
            await _dbContext.SaveChangesAsync();
            return county;
        }

        public async Task<County?> DeleteAsync(Guid id)
        {
            var countyExists = await _dbContext.Counties.FirstOrDefaultAsync(c => c.Id == id);

            if (countyExists == null)
            {
                return null;
            }

            _dbContext.Remove(countyExists);
            await _dbContext.SaveChangesAsync();
            return countyExists;
        }

        public async Task<List<County>> GetAllAsync()
        {
            return await _dbContext.Counties.Include(c => c.City).ToListAsync();
        }

        public async Task<County?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Counties.
                Include(c => c.City).
                FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<County?> UpdateAsync(Guid id, County county)
        {
            var getCounty = await _dbContext.Counties.FirstOrDefaultAsync(c => c.Id == id);

            if (getCounty == null)
            {
                return null;
            }

            getCounty.Name = county.Name;
            await _dbContext.SaveChangesAsync();

            return getCounty;
        }
    }
}
