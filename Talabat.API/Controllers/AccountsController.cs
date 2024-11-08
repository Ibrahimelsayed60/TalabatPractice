using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;

namespace Talabat.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto model)
        {
            var User = new AppUser() 
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split("@")[0]
            };

            var Result = await _userManager.CreateAsync(User, model.Password);
            if(!Result.Succeeded)  return BadRequest(new ApiResponse(400));

            var ReturnedUser = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };

            return Ok(ReturnedUser);
        }


        // Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if(User is null) return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);

            if(!Result.Succeeded) return Unauthorized(new ApiResponse(401));

            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _tokenService.CreateTokenAsync(User, _userManager)
            };

            return Ok(ReturnedUser);

        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var ReturnedObject = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(ReturnedObject);
        }


    }
}
