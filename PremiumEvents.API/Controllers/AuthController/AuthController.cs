using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PremiumEvents.API.Models.DTOs.AuthDtos;
using PremiumEvents.API.Repos;

namespace PremiumEvents.API.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenInterface _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, TokenInterface tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Assign the "Client" role to the new user
                var roleResult = await _userManager.AddToRoleAsync(identityUser, "Client");

                if (roleResult.Succeeded)
                {
                    return Ok("User successfully registered with 'Client' role! You can now login!");
                }
                else
                {
                    // Optionally handle the case where assigning the role failed
                    return BadRequest("User created but failed to assign 'Client' role.");
                }
            }

            return BadRequest("Something went wrong");
        }




        //  [HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        //{
        //   var identityUser = new IdentityUser
        //  {
        //     UserName = registerRequestDto.Username,
        //    Email = registerRequestDto.Username
        //};

        //var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

        //if (identityResult.Succeeded)
        //{
        //  //Add roles to this User 
        //if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
        //{
        //  identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
        //
        //if (identityResult.Succeeded)
        //{
        //   return Ok("User successfully registered! You can now login!");
        //}
        //}
        // }

        //return BadRequest("Something went wrong");
        //}

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null) 
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token

                        var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);

                    }
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
