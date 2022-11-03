using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using BackendDev.Data.Models;
using XAct.Users;

namespace BackendDev.Data.ViewModels
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        [MinLength(4)]
        [MaxLength(32)]
        public string? NickName { get; set; }
        [Required(ErrorMessage = "Не указана почта пользователя")]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
        public string Email { get; set; }
        public string? AvatarLink { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public string BirthDate { get; set; }
        public Gender Gender  { get; set; }
        public ProfileModel(Guid id, string? nickName, string email, string? avatarLink, string name, string birthDate, Gender gender)
        {
            Id = id;
            NickName = nickName;
            Email = email;
            AvatarLink = avatarLink;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
        }
        public ProfileModel(UserModel modelDTO)
        {
            Id = modelDTO.Id;
            NickName = modelDTO.UserName;
            Email = modelDTO.Email;
            AvatarLink = modelDTO.AvatarLink;
            Name = modelDTO.Name;
            BirthDate = modelDTO.BirthDate;
            Gender = modelDTO.Gender;
        }
        public ProfileModel()
        {

        }
    }
}
