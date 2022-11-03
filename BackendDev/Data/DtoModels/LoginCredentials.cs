using System.ComponentModel.DataAnnotations;

namespace BackendDev.Data.ViewModels
{
    public class LoginCredentials
    {
        [MinLength(6)]
        [MaxLength(32)]
        public string? UserName { get; set; }
        [MinLength(6)]
        [MaxLength(32)]
        public string? Password { get; set; }
        public LoginCredentials(UserRegisterModel model)
        {
            UserName = model.UserName;
            Password = model.Password;
        }
        public LoginCredentials()
        {

        }
    }
}
