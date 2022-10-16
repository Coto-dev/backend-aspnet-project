using System.ComponentModel.DataAnnotations;

namespace BackendDev.Data.ViewModels
{
    public class ReviewModifyModel
    {
        [Required(ErrorMessage = "Пустое поле отзыва")]
        public string ReviewText { get; set; }
        [Range(1,10,ErrorMessage = "Превышен диапозон(от 1 до 10)")]
        public int Rating { get; set; }
        public bool isAnonymous { get; set; }
    }
}
