using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
namespace BackendDev.Data.Models
{
    public class UserModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataType(DataType.DateTime)]
        public string BirthDate { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Не указана почта пользователя")]
        [EmailAddress(ErrorMessage = "Некорректный адрес почты")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? AvatarLink { get; set; }
       public bool IsAdmin { get; set; }
        public Gender Gender { get; set; }
        public List<MovieModel> FavouriteMovies { get; set; }  = new List<MovieModel>();
        public List<ReviewModelBd> Reviews { get; set; } = new List<ReviewModelBd>();
    }

}
