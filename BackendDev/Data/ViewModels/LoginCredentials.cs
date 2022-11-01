namespace BackendDev.Data.ViewModels
{
    public class LoginCredentials
    {
        public string? UserName { get; set; }
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
