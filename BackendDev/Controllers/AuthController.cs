using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackendDev.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AuthController : Controller
    {

        private IAuthService _authService;
        private ContextDataBase _contextData;
        public AuthController(IAuthService authService,ContextDataBase contextData)
        {
            _authService = authService;
            _contextData = contextData;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(UserRegisterModel model)
        {
            if (!ModelState.IsValid) //Проверка полученной модели данных
            {
                return StatusCode(401, "Model is incorrect");
            }

            try
            {
                await _authService.Add(model);
                return Ok();
            }
            catch (Exception ex)
            {
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during adding user");
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Token([FromForm] LoginCredentials model)
        {
            var identity = GetIdentity(model.UserName, model.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: JwtConfigurations.Issuer,
                audience: JwtConfigurations.Audience,
                notBefore: now,
            claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(JwtConfigurations.Lifetime)),
                signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _contextData.Users.FirstOrDefault(x => x.UserName == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            // Claims описывают набор базовых данных для авторизованного пользователя
            var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
        };

            //Claims identity и будет являться полезной нагрузкой в JWT токене, которая будет проверяться стандартным атрибутом Authorize
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        [HttpGet]
        [Authorize]//Данный Endpoint доступен только для авторизованных пользователей
        [Route("test_login")]
        public IActionResult TestAuth()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("test_admin")]
        public IActionResult TestAdmin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }


        /*public async Task<IActionResult> Create(UserModel user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok("good");
        }*/
    }
}
