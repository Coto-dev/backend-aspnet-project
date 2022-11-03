using BackendDev.Data;
using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public UserService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
        public ProfileModel GetProfile(string UserName)
        {
            var profile = _contextData.Users.FirstOrDefault(x=>x.UserName == UserName);
            if (profile != null)
            return new ProfileModel(profile);
            else throw new ArgumentException("Такого пользователя не существует");

        }
        public async Task EditProfile(ProfileModel modelDto)
        {
            /*foreach (UserModel user in _contextData.Users)
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
            }*/
            var user = await _contextData.Users.FirstOrDefaultAsync(x => x.Id == modelDto.Id);
            if (user != null)
            {
                user.UserName = modelDto.NickName;
                user.Email = modelDto.Email;
                user.AvatarLink = modelDto.AvatarLink;
                user.Name = modelDto.Name;
                user.BirthDate = modelDto.BirthDate;
                user.Gender = modelDto.Gender;
            }
            else throw new ArgumentException("Такого пользователя не существует");
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
