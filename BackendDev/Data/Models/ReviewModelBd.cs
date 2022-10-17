using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendDev.Data.Models
{
    public class ReviewModelBd
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Пустое поле отзыва")]
        public string ReviewText { get; set; }
        [Range(1, 10, ErrorMessage = "Превышен диапозон(от 1 до 10)")]
        public int Rating { get; set; }
        public bool isAnonymous { get; set; }
        [DataType(DataType.DateTime)]
        public string CreateDateTime { get; set; }
    }
}
