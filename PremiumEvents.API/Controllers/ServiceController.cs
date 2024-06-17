using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PremiumEvents.API.Models.Domain;
using PremiumEvents.API.Models.DTOs.ServiceDtos;
using PremiumEvents.API.Repos;
using System.Security.Claims;

namespace PremiumEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceInterface _serviceRepos;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public ServiceController(ServiceInterface serviceRepos, IMapper mapper, IAuthorizationService authorizationService)
        {
            _serviceRepos = serviceRepos;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            var service = await _serviceRepos.GetAllAsync();

            var serviceDto = _mapper.Map<List<ServiceDto>>(service);

            return Ok(serviceDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetServiceById([FromRoute] Guid id)
        {
            var service = await _serviceRepos.GetByIdAsync(id);

            if ( service == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ServiceDto>(service));
        }

        [HttpPost]
        [Authorize(Roles = "Master,Admin")]
        public async Task<IActionResult> CreateService([FromBody] AddServiceDto addServiceDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Ensure userId is not null or empty
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to determine the user making the request.");
            }


            var service = _mapper.Map<Service>(addServiceDto);
            service.CreatedBy = userId;

            service = await _serviceRepos.CreateAsync(service);

            var serviceDto = _mapper.Map<ServiceDto>(service);

            return Ok(serviceDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master,Admin")]
        public async Task<IActionResult> UpdateService([FromRoute] Guid id, [FromBody] UpdatedServiceDto updateServiceDto)
        {
            var service = await _serviceRepos.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, service, "ModifyService");

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(updateServiceDto, service);

            service = await _serviceRepos.UpdateAsync(id, service);

            var serviceDto = _mapper.Map<ServiceDto>(service);

            return Ok(serviceDto);
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Master,Admin")]
        public async Task<IActionResult> DeleteService([FromRoute] Guid id)
        {
            var service = await _serviceRepos.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, service, "ModifyService");

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            service = await _serviceRepos.DeleteAsync(id);

            var serviceDto = _mapper.Map<ServiceDto>(service);

            var response = new
            {
                Name = serviceDto.Name,
                Deleted = true
            };

            return Ok(response);
        }
    }
}
