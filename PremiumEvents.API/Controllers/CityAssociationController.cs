using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiumEvents.API.Data;
using PremiumEvents.API.Models.Domain;

namespace PremiumEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityAssociationController : ControllerBase
    {
        private readonly PremiumEventsDbContext _dbContext;

        public CityAssociationController(PremiumEventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("AddServiceToCity")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> AddServiceToCity(Guid cityId, Guid serviceId)
        {
            var cityService = new CityService
            {
                CityId = cityId,
                ServiceId = serviceId
            };

            _dbContext.CityServices.Add(cityService);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("AddServiceCategoryToCity")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> AddServiceCategoryToCity(Guid cityId, Guid serviceCategoryId)
        {
            var cityServiceCategory = new CityServiceCategory
            {
                CityId = cityId,
                ServiceCategoryId = serviceCategoryId
            };

            _dbContext.CityServiceCategories.Add(cityServiceCategory);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
