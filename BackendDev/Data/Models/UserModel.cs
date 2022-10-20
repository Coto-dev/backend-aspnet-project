using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
namespace BackendDev.Data.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [DataType(DataType.DateTime)]
        public string BirthDate { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана почта пользователя")]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; } = "user";
        public string? AvatarLink { get; set; }
       public bool IsAdmin { get; set; }
        public Gender Gender { get; set; }
        public List<MovieModel> UserMovies { get; set; }  = new List<MovieModel>();
        public List<ReviewModelBd> Reviews { get; set; } = new List<ReviewModelBd>();
    }

}
