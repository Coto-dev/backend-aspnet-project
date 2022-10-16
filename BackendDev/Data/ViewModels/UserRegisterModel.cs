using System.ComponentModel.DataAnnotations;
using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class UserRegisterModel
    {
        [Required (ErrorMessage = "Не указан ник пользователя")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан пароль пользователя")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Не указана почта пользователя")]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
        public string Email { get; set; }
        [DataType(DataType.DateTime)]
        public string BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
