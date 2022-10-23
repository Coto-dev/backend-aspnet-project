using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendDev.Services
{
    public interface IUserService
    {
        public ProfileModel GetProfile(string UserName);
        Task<bool> CheckToken(HttpRequest httpRequest);
        Task EditProfile(ProfileModel modelDto);
    }
    public class UserService : IUserService
    {
        private readonly ContextDataBase _contextData;
       // private readonly ILogger _logger;
        public UserService(ContextDataBase contextData)
        {
            _contextData = contextData;
          //  _logger = logger;   
        }
        public ProfileModel GetProfile(string UserName)
        {
            foreach (UserModel user in _contextData.Users)
            {
                if (user.UserName == UserName)
                {
                    var profile = new ProfileModel
                    {
                        Id = user.Id.ToString(),
                        NickName = user.UserName,
                        Email = user.Email,
                        AvatarLink = user.AvatarLink,
                        Name = user.Name,
                        BirthDate = user.BirthDate,
                        Gender = user.Gender

                    };
                    return profile;
                }
               /* else
                {
                    throw new ArgumentException("Такого пользователя не существует");
                }*/
            }
            return null;
        }
        public async Task EditProfile(ProfileModel modelDto)
        {
            foreach (UserModel user in _contextData.Users)
            {
                if (user.Id.ToString() == modelDto.Id.ToString())
                {
                    user.UserName = modelDto.NickName;
                    user.Email = modelDto.Email;
                    user.AvatarLink = modelDto.AvatarLink;  
                    user.Name = modelDto.Name;
                    user.BirthDate = modelDto.BirthDate;
                    user.Gender = modelDto.Gender;
                }
            }
            await _contextData.SaveChangesAsync();
        }
        public Task<Boolean> CheckToken(HttpRequest httpRequest)
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
        }
    }
}
