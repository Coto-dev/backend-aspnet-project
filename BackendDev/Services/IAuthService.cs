using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BackendDev.Data;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using XSystem.Security.Cryptography;

namespace BackendDev.Services
{
    public interface IAuthService
    {

        public Task<JsonResult> Add(UserRegisterModel RegisterModelDto);
        public JsonResult Login(LoginCredentials LoginDto);
        public Task Logout(HttpRequest httpRequest);
       // public Task<Boolean> CheckToken(HttpRequest httpRequest);
        public JsonResult Token(LoginCredentials LoginDto);
       
    }
    public class AuthService : IAuthService
    {
       private readonly ContextDataBase _contextData;
        public AuthService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }

        public JsonResult Login(LoginCredentials LoginDto)
        {
            LoginDto.Password = GetHashPasword(LoginDto.Password);
           return Token(LoginDto);
        }

        public async Task<JsonResult> Add(UserRegisterModel RegisterModelDto)
        {
            if (await _contextData.Users.FirstOrDefaultAsync(x => x.UserName == RegisterModelDto.UserName) == null)
            {
                if (await _contextData.Users.FirstOrDefaultAsync(x => x.Email == RegisterModelDto.Email) == null)
                {   
                    RegisterModelDto.Password = GetHashPasword(RegisterModelDto.Password);
                    await _contextData.Users.AddAsync(new UserModel(RegisterModelDto));
                    await _contextData.SaveChangesAsync();
                    return Token(new LoginCredentials(RegisterModelDto));
                }
                else throw new ArgumentException("Пользователь с такой почтой уже существует");
            }
            else throw new ArgumentException("Пользователь с таким UserName уже существует");
        }
        public static string GetHashPasword(string password)
        {
            var Bytes = ASCIIEncoding.ASCII.GetBytes(password);
            var Hash = new MD5CryptoServiceProvider().ComputeHash(Bytes);
            return Encoding.UTF8.GetString(Hash);
        }

        public JsonResult Token(LoginCredentials model)
        {
            var identity = GetIdentity(model.UserName, model.Password);
            if (identity == null)
            {
                 throw new ArgumentException("неверное имя пользователя или пароль");
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
            //return encodedJwt;
            var response = new
            {
                token = encodedJwt
            };

            return new JsonResult( response);
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

      /*  public Task<Boolean> CheckToken(HttpRequest httpRequest)
        {
            var token = httpRequest.Headers["Authorization"];
            foreach (InvalidToken InvToken in _contextData.InvalidTokens)
            {
                if (InvToken.JWTToken == token)
                {
                    return Task.FromResult(false);
                }
                else return Task.FromResult(true);
            }
            return Task.FromResult(true);
        }*/

        public async Task Logout(HttpRequest httpRequest)
        {
            var token = httpRequest.Headers["Authorization"];
            await _contextData.InvalidTokens.AddAsync(new InvalidToken
            {
                JWTToken = token.ToString()
            });
            await _contextData.SaveChangesAsync();

        }

        /* public async Task<IActionResult> Add(UserRegisterModel RegisterModelDto)
         {
             if (await _contextData.Users.FirstOrDefaultAsync(x => x.UserName == RegisterModelDto.UserName) == null)
             {
                 if (await _contextData.Users.FirstOrDefaultAsync(x => x.Email == RegisterModelDto.Email) == null)
                 {
                     await _contextData.Users.AddAsync(new UserModel(RegisterModelDto));
                     await _contextData.SaveChangesAsync();
                    return await CreateToken(new LoginCredentials(RegisterModelDto));
                 }
                 else throw new ArgumentException("Пользователь с такой почтой уже существует");
             }
             else throw new ArgumentException("Пользователь с таким UserName уже существует");
         }
         public async Task<IActionResult> Login(LoginCredentials LoginDto)
         {
             var token = await CreateToken(LoginDto);            
             return token;
         }
         public async Task Logout(HttpRequest httpRequest)
         {
             var token =  httpRequest.Headers["Authorization"];
             await _contextData.InvalidTokens.AddAsync(new InvalidToken
             {
                 JWTToken = token.ToString()
             });
             await _contextData.SaveChangesAsync();

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

         public async Task <IActionResult> CreateToken(LoginCredentials LoginDto)
         {
             var identity = await GetIdentity(LoginDto.UserName, LoginDto.Password);
             if (identity == null)
             {
                 throw new ArgumentException("такого пользователя не существует");
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
             //return encodedJwt;
             var response = new
             {
                 token = encodedJwt
             };

            return new JsonResult(response);
         }
         private async Task <ClaimsIdentity> GetIdentity(string username, string password)
         {
             var user  = await _contextData.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
             if (user == null)
             {
                 throw new ArgumentException("такого пользователя не существует");
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
         }*/
    }
}
