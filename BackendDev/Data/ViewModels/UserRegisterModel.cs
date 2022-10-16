using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class UserRegisterModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
