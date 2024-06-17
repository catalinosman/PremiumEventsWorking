using Microsoft.EntityFrameworkCore;
using PremiumEvents.API.Data;
using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Repos.Implementations
{
    public class CityImplementation : CityInterface
    {
        private readonly PremiumEventsDbContext _dbContext;

        public CityImplementation(PremiumEventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<City> CreateAsync(City city)
        {
            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();
            return city;
        }

        public async Task<City?> DeleteAsync(Guid id)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
            {
                return null;
            }

            _dbContext.Remove(city);
            await _dbContext.SaveChangesAsync();
            return city;
        }


        //test 
        public async Task<List<City>> GetAllAsync()
        {
            return await _dbContext.Cities
                           .Include(c => c.CityServices)
                               .ThenInclude(cs => cs.Service)
                           .Include(c => c.CityServiceCategories)
                               .ThenInclude(csc => csc.ServiceCategory)
                           .ToListAsync();
        }

        public async Task<City?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Cities
                           .Include(c => c.CityServices)
                               .ThenInclude(cs => cs.Service)
                           .Include(c => c.CityServiceCategories)
                               .ThenInclude(csc => csc.ServiceCategory)
                           .FirstOrDefaultAsync(c => c.Id == id);
        }
   
        public async Task<City?> UpdateAsync(Guid id, City city)
        {
            var cityExists = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if(cityExists == null)
            {
                return null;
            }

            cityExists.Name = city.Name;
            await _dbContext.SaveChangesAsync();

            return cityExists;
        }
    }
}
