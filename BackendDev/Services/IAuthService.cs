using BackendDev.Data.Models;
using BackendDev.Data.ViewModels;

namespace BackendDev.Services
{
    public interface IAuthService
    {
        UserRegisterModel[] GetUserRegisterModel();
        Task Add(UserRegisterModel model);
    }
    public class AuthService : IAuthService
    {
       private readonly ContextDataBase _contextData;
        public AuthService(ContextDataBase contextData)
        {
            _contextData = contextData;
        }
        public UserRegisterModel[] GetUserRegisterModel()
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
        }
        public async Task Add(UserRegisterModel model)
        {
            await _contextData.Users.AddAsync(new UserModel
            {
                UserName = model.UserName,
                Name = model.UserName,
                Password = model.Password,
                Email = model.Email,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
            });
            await _contextData.SaveChangesAsync();
        }
    }
}
