using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Nodes;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using BackendDev.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
                var response = await _authService.Add(RegisterModelDto);
                if (response != null)
                    return Ok(response.Value);
                else return BadRequest(new { errorText = "Invalid username or password." });
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during adding user");
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginCredentials LoginDto)
        {
            if (!ModelState.IsValid) //Проверка полученной модели данных
            {
                return StatusCode(401, "Model is incorrect");
            }

            try
            {
                var response = _authService.Login(LoginDto);
                if (response!= null)
                return Ok(response.Value);
                else return BadRequest(new { errorText = "Invalid username or password." });

            }
            catch (Exception ex)
            {
                // TODO: Добавить логирование
                return StatusCode(500, "Errors during login user");
            }
           
        }

       

        [HttpGet]
        [Authorize]//Данный Endpoint доступен только для авторизованных пользователей
        [Route("test_login")]
        public async Task <IActionResult> TestAuth()
        {
           var TokenIsValid = await _authService.CheckToken(Request);
            if (TokenIsValid)
            return Ok($"Ваш логин: {User.Identity.Name}");
            else return BadRequest(new { errorText = "Invalid token" });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("test_admin")]
        public IActionResult TestAdmin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }


        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(Request);
           return Ok();

        }
    }
}
