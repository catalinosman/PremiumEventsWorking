using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiumEvents.API.Models.Domain;
using PremiumEvents.API.Models.DTOs.CityDtos;
using PremiumEvents.API.Models.DTOs.CountyDtos;
using PremiumEvents.API.Repos;

namespace PremiumEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityInterface _cityRepos;
        private readonly IMapper _mapper;

        public CityController(CityInterface cityRepos, IMapper mapper)
        {
            _cityRepos = cityRepos;
            _mapper = mapper;
        }

        [HttpGet("All Cities")]
        public async Task<IActionResult> GetAllCities() 
        {
            var city = await _cityRepos.GetAllAsync();

            var cityDto = _mapper.Map<List<CityContainsDto>>(city);

            return Ok(cityDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCityById([FromRoute] Guid id)
        {
            var city = await _cityRepos.GetByIdAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CityContainsDto>(city));
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> CreateCity([FromBody] AddCityDto addCityDto)
        {
            var city = _mapper.Map<City>(addCityDto);

            city = await _cityRepos.CreateAsync(city);

            var cityDto = _mapper.Map<RequestCityDto>(city);

            return Ok(cityDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> UpdateCity([FromRoute] Guid id, [FromBody] RequestCityDto requestCityDto)
        {
            var city = _mapper.Map<City>(requestCityDto);

            city = await _cityRepos.UpdateAsync(id, city);

            if (city == null)
            {
                return NotFound();
            }

            var cityDto = _mapper.Map<UpdateCityDto>(city);

            return Ok(cityDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _cityRepos.DeleteAsync(id);

            if ( city == null )
            {
                return NotFound();
            }

            var cityDto = _mapper.Map<CityDto>(city);

            var response = new
            {
                Name = cityDto.Name,
                Deleted = true // You can set this to true to indicate successful deletion
            };

            return Ok(response);
        }
    }
}
