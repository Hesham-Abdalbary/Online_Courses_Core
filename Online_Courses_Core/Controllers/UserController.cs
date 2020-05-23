using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Online_Courses_Core.BindingModels;
using Online_Courses_Core.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Online_Courses_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Online_Courses_Core.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class UserController : Controller
    {
        private UserService _userService;
        private IConfiguration _config;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        Context _context;
        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }
        public UserController(UserService userService, UserManager<User> userManager,
            SignInManager<User> signInManager, Context context, IConfiguration config)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _config = config;
        }

        [HttpGet]
        public ActionResult Get()
        {
           var users= _userService.getAllUsers();
            return Ok(users);
        }


        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterBindingModel model)
        {
            await _userService.AddUser(model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Token")]
        public async Task<IActionResult> Login([FromBody]UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(login);
            var tokenString =await GenerateJSONWebToken(user);
            return Ok(tokenString);
        }

        private async Task<object> GenerateJSONWebToken(User user)
        {
            var utcNow = DateTime.UtcNow;
            var claims = new Claim[]
          {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
          };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
                    notBefore: utcNow,
                    expires: DateTime.Now.AddMinutes(120),
                    audience: _config["Jwt:Issuer"],
                    issuer: _config["Jwt:Issuer"]
                );

            return await BuildTokenObject(user, jwt, utcNow);
        }

        private async Task<User> AuthenticateUser(UserModel autheticationBindingModel)
        {
            var user = await _userManager.FindByNameAsync(autheticationBindingModel.UserName);
            if (user == null)
                throw new Exception("InvalidLogin");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, autheticationBindingModel.Password, user.LockoutEnabled);
            if (signInResult.Succeeded == false)
                throw new Exception("InvalidLogin");

            return user;
        }

        private async Task<object> BuildTokenObject(User user, JwtSecurityToken jwt, DateTime utcNow)
        {
            var roles = await _userManager.GetRolesAsync(user);
            // var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.UserName);

            return new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(jwt),
                token_type = "bearer",
                expires_in = utcNow.AddSeconds(60),
                // refresh_token = refreshToken,
                //client_id = clientId,
               // userName = user.UserName,
                //name = user.UserName,
                role = roles.ToList(),
                //firstName = user.FirstName,
                //lastName = user.LastName,
               // email = user.Email,
                issued = utcNow,
                expires = utcNow.AddSeconds(60)
            };
        }
    }
}
