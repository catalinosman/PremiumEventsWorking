using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiumEvents.API.Models.Domain;
using PremiumEvents.API.Models.DTOs.CountyDtos;
using PremiumEvents.API.Repos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PremiumEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : ControllerBase
    {
        private readonly CountyInterface _countyRepos;
        private readonly IMapper _mapper;
        private readonly ILogger<CountyController> _logger;

        public CountyController(CountyInterface countyRepos, IMapper mapper, ILogger<CountyController> logger)
        {
            _countyRepos = countyRepos;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAllCounties action method was invoked");

            _logger.LogWarning("This is a warning log");

            _logger.LogError("This is an error log");

            var county = await _countyRepos.GetAllAsync();

            var countyDto = _mapper.Map<List<CountyDto>>(county);

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 64 // Optional: adjust the max depth if necessary
            };

            _logger.LogInformation($"Finished GetAllCounties request with data{JsonSerializer.Serialize(county, jsonOptions)}");

            return Ok(countyDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var county = await _countyRepos.GetByIdAsync(id);

            if(county == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CountyDto>(county));
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> CreateCounty([FromBody] RequestCountyDto requestCountyDto)
        {
            var county = _mapper.Map<County>(requestCountyDto);

            county = await _countyRepos.CreateAsync(county);

            var countyDto = _mapper.Map<UpdateCountyDto>(county);

            return Ok(countyDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> UpdateCounty([FromRoute] Guid id, [FromBody] RequestCountyDto updateCountyDto)
        {
            var county = _mapper.Map<County>(updateCountyDto);

            county = await _countyRepos.UpdateAsync(id, county);

            if(county == null)
            {
                return NotFound();
            }

            var countyDto = _mapper.Map<UpdateCountyDto>(county);

            return Ok(countyDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> DeleteCounty(Guid id)
        {
            var county = await _countyRepos.DeleteAsync(id);

            if(county == null)
            {
                return NotFound();
            }

            var countyDto = _mapper.Map<RequestCountyDto>(county);

            var response = new
            {
                Name = countyDto.Name,
                Deleted = true // You can set this to true to indicate successful deletion
            };

            return Ok(response);

        }
    }
}
