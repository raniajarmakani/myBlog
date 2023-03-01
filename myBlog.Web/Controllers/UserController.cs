using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.User;
using MyBlog.Services;
using System.Threading.Tasks;

namespace myBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;

        public UserController(ITokenService ts, UserManager<UserIdentity> um, SignInManager<UserIdentity> sm)
        {
            _tokenService = ts;
            _userManager = um;
            _signInManager = sm;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreate userCreate)
        {
            var userIdentity = new UserIdentity
            {
                UserName = userCreate.Username,
                Email = userCreate.Email,
                FullName = userCreate.Fullname
            };
            var result = await _userManager.CreateAsync(userIdentity, userCreate.Password);
            if (result.Succeeded)
            {
                User user = new User
                {
                    UserID = userIdentity.UserID,
                    Username = userIdentity.UserName,
                    Email = userIdentity.Email,
                    Fullname = userIdentity.FullName,
                    Token = _tokenService.CreateToken(userIdentity)
                };
                return Ok(user);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login (UserLogin userLogin)
        {
            var userIdentity = await _userManager.FindByNameAsync(userLogin.Username);
            if (userIdentity != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(userIdentity, userLogin.Password, false);
                if (result.Succeeded)
                {
                    User user = new User
                    {
                        UserID = userIdentity.UserID,
                        Username = userIdentity.UserName,
                        Email = userIdentity.Email,
                        Fullname = userIdentity.FullName,
                        Token = _tokenService.CreateToken(userIdentity)
                    };

                    return Ok(user);
                }

            }
            return BadRequest("Invalid Login attempt");
        }
    }
}
