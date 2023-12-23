using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies_Watchlist_API.JwtBearer;
using Movies_Watchlist_API.Models;
using Movies_Watchlist_DB.Models;

namespace Movies_Watchlist_API.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("testApi")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<MovieUser> signInManager;
        private readonly UserManager<MovieUser> userManager;
        private readonly TokenService tokenService;

        public AuthController(SignInManager<MovieUser> signInManager, UserManager<MovieUser> userManager, TokenService tokenService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }   


        [HttpPost("user")] // REGISTRATION
        public async Task<IActionResult> RegisterUser(AuthDto authDto)
        {

            var newUser = new MovieUser
            {
                UserName = authDto.Email,
                Email = authDto.Email
            };

            var result = await userManager.CreateAsync(newUser, authDto.Password);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(authDto.Email);
                var userDto = new UserDto { UserId = user.Id, Email = user.Email };
                var token = tokenService.GenerateToken(userDto);
                var userName = userDto.Email.Split('@')[0];
                return Ok(new { Token = token,User = userName });
             
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("auth")]  // LOGIN
        public async Task<IActionResult> LogInUser(AuthDto authDto)
        {
            MovieUser user = await userManager.FindByEmailAsync(authDto.Email);

            if (user is null)
                return NotFound();


            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, authDto.Password, false, false);

            if (result.Succeeded)
            {
                var userDto = new UserDto { UserId = user.Id, Email = user.Email };
                var token = tokenService.GenerateToken(userDto);
                var userName = userDto.Email.Split('@')[0];
                return Ok(new { Token = token, User = userName });
            }

            return BadRequest();

        }

        [HttpDelete("auth")] //LOGOUT
        public async Task<IActionResult> LogOutUser()
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

    }
}
