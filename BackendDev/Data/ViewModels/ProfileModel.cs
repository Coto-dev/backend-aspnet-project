using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class ProfileModel
    {
        public string Id { get; set; }
        public string? NickName { get; set; }
        [Required(ErrorMessage = "Не указана почта пользователя")]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
        public string Email { get; set; }
        public string? AvatarLink { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public string BirthDate { get; set; }
        public Gender Gender  { get; set; }

    }
}
