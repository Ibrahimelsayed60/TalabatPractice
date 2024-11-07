using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                Token = "ThisWillBeToken"
            };

            return Ok(ReturnedUser);
        }


        // Login

    }
}
