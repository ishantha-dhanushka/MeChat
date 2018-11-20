using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MeChatDTO;
using MeChatRepository;
using MeChatRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MeChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private UserManager<AppUser> userManager;
        private IHttpContextAccessor httpContextAccessor; 
        private IUserRepository userRepository; 

        public AuthController(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mechatsecurekey@1234"));
                var token = new JwtSecurityToken(
                        issuer: "http://mechat.com",
                        audience: "http://mechat.com",
                        expires: DateTime.UtcNow.AddDays(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Get Logged user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user")]
        public new async Task<IActionResult> User()
        {
            AppUser user = await userRepository.GetLoggedUser();
            return Ok(new {
                username = user.UserName,
                id = user.Id,
                name = user.Name
            });
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IdentityResult> Register(RegisterDTO user)
        {
            // create appUser object
            AppUser appUser = new AppUser {
                UserName = user.Username,
                Name = user.Name
            };
            return await userRepository.Create(appUser, user.Password);
        }
    }
}