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
        public Task<bool> CheckToken(string token);
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
        public async Task<bool> CheckToken(string token)
        {
            var FindedToken = await _contextData.InvalidTokens.FirstOrDefaultAsync(x => x.JWTToken == token);
            if (FindedToken != null)
                return false;
            else return true;
        }
       
    }
}
