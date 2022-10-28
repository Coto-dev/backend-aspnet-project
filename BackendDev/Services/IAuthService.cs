using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackendDev.Services
{
    public interface IAuthService
    {
        /*UserRegisterModel[] GetUserRegisterModel();*/
        Task Add(UserRegisterModel RegisterModelDto);
        Task<IActionResult> Login(LoginCredentials LoginDto);
        Task Logout(HttpRequest httpRequest);
        Task<bool> CheckToken(HttpRequest httpRequest);
    }
    public class AuthService : IAuthService
    {
       private readonly ContextDataBase _contextData;
        public AuthService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
       /* public UserRegisterModel[] GetUserRegisterModel()
        {
            return _contextData.Users.Select(x=> new UserRegisterModel
            {
                UserName = x.UserName,
                Name = x.Name,
                Password = x.Password,
                Email = x.Email,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
            }).ToArray();
        }*/
        public async Task Add(UserRegisterModel RegisterModelDto)
        {
            foreach (UserModel user in _contextData.Users){
                if (user.UserName == RegisterModelDto.UserName.ToLower())
                {
                    throw new ArgumentException("Такой пользователь уже существует");
                }
            }
            await _contextData.Users.AddAsync(new UserModel
            {
                UserName = RegisterModelDto.UserName,
                Name = RegisterModelDto.UserName,
                Password = RegisterModelDto.Password,
                Email = RegisterModelDto.Email,
                BirthDate = RegisterModelDto.BirthDate,
                Gender = RegisterModelDto.Gender,
            });
            await _contextData.SaveChangesAsync();
        }
        public async Task<IActionResult> Login(LoginCredentials LoginDto)
        {
            var identity = await GetIdentity(LoginDto.UserName, LoginDto.Password);
            if (identity == null)
            {
                return null;//(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: JwtConfigurations.Issuer,
                audience: JwtConfigurations.Audience,
                notBefore: now,
            claims: identity.Claims,
                expires:  now.Add(TimeSpan.FromMinutes(JwtConfigurations.Lifetime)),
                signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                username =  identity.Name
            };

            return new JsonResult(response);
        }
        public async Task Logout(HttpRequest httpRequest)
        {
            var token =  httpRequest.Headers["Authorization"];
            await _contextData.InvalidTokens.AddAsync(new InvalidToken
            {
                JWTToken = token.ToString()
            });
            await _contextData.SaveChangesAsync();

            /*  var token = await httpRequest.ReadFormAsync(Headers["Authorization"]);*/
        }
        public Task<Boolean> CheckToken(HttpRequest httpRequest)
        {
            var token = httpRequest.Headers["Authorization"];
            foreach ( InvalidToken InvToken in  _contextData.InvalidTokens)
            {
                if (InvToken.JWTToken ==  token)
                {
                    return Task.FromResult(false);
                }
                else return Task.FromResult(true);
            }
            return Task.FromResult(true);
        }

        private async Task <ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user  = await _contextData.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
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
    }
}
