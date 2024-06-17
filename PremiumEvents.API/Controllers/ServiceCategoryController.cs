using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiumEvents.API.Models.Domain;
using PremiumEvents.API.Models.DTOs.CityDtos;
using PremiumEvents.API.Models.DTOs.CountyDtos;
using PremiumEvents.API.Models.DTOs.ServiceCategoryDtos;
using PremiumEvents.API.Repos;

namespace PremiumEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryController : ControllerBase
    {
        private readonly ServiceCategoryInterface _serviceCategoryRepos;
        private readonly IMapper _mapper;

        public ServiceCategoryController(ServiceCategoryInterface serviceCategoryRepos, IMapper mapper)
        {
            _serviceCategoryRepos = serviceCategoryRepos;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _serviceCategoryRepos.GetAllAsync();

            var categorydto = _mapper.Map<List<ServiceCategoryDto>>(category);

            return Ok(categorydto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var category = await _serviceCategoryRepos.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServiceCategoryDto>(category));
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] AddServiceCategoryDto addServiceCategory)
        {
            var category = _mapper.Map<ServiceCategory>(addServiceCategory);

            category = await _serviceCategoryRepos.CreateAsync(category);

            var categoryDto = _mapper.Map<DisplayServiceCategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] DisplayServiceCategoryDto requestCategoryDto)
        {
            var category = _mapper.Map<ServiceCategory>(requestCategoryDto);

            category = await _serviceCategoryRepos.UpdateAsync(id, category);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<UpdateServiceCategoryDto>(category);

            return Ok(categoryDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _serviceCategoryRepos.DeleteAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<DisplayServiceCategoryDto>(category);

            // Create an object containing both the name of the deleted service and a deletion confirmation flag
            var response = new
            {
                Name = categoryDto.Name,
                Deleted = true // You can set this to true to indicate successful deletion
            };

            return Ok(response);
        }
    }
}
