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
        public async Task<IActionResult> Post(UserRegisterModel RegisterModelDto)
        {
            if (!ModelState.IsValid) //Проверка полученной модели данных
            {
                return StatusCode(401, "Model is incorrect");
            }

            try
            {
                await _authService.Add(RegisterModelDto);
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
        public  async Task<IActionResult> Token([FromBody]LoginCredentials LoginDto)
        {
            if (!ModelState.IsValid) //Проверка полученной модели данных
            {
                return StatusCode(401, "Model is incorrect");
            }

            try
            {
                var response = await _authService.Login(LoginDto);
                if (response!= null)
                return response;
                else return BadRequest(new { errorText = "Invalid username or password." });

            }
            catch (Exception ex)
            {
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during login user");
            }
            /*var identity = GetIdentity(LoginDto.UserName, LoginDto.Password);
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

            return new JsonResult(response);*/
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
