using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using BackendDev.Data.ViewModels;

namespace BackendDev.Data.Models
{
    public class ReviewModelBd//film to review , review to user
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Пустое поле отзыва")]
        public string ReviewText { get; set; }
        [Range(1, 10, ErrorMessage = "Превышен диапозон(от 1 до 10)")]
        public int Rating { get; set; }
        public bool isAnonymous { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDateTime = DateTime.Now;
        [Required]
        public UserModel User { get; set; }

        public ReviewModelBd(ReviewModifyModel reviewModelDTO, UserModel user)
        {
            ReviewText = reviewModelDTO.ReviewText;
            Rating = reviewModelDTO.Rating;
            isAnonymous = reviewModelDTO.isAnonymous;
            User = user;
        }
        public ReviewModelBd(Guid reviewId, ReviewModifyModel reviewModelDTO, UserModel user)
        {
            Id=reviewId;
            ReviewText = reviewModelDTO.ReviewText;
            Rating = reviewModelDTO.Rating;
            isAnonymous = reviewModelDTO.isAnonymous;
            User = user;
        }
        public ReviewModelBd()
        {

        }
    }
}
