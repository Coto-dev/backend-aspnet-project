using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class ReviewModel
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime CreateDateTime { get; set; }
        public UserShortModel Author { get; set; }
        public ReviewModel(string id, int rating, string? reviewText, bool isAnonymous, DateTime createDateTime, UserShortModel author)
        {
            Id = id;
            Rating = rating;
            ReviewText = reviewText;
            IsAnonymous = isAnonymous;
            CreateDateTime = createDateTime;
            Author = author;
        }
        public ReviewModel(ReviewModelBd reviewModelBd)
        {
            Id = reviewModelBd.Id.ToString();
            Rating = reviewModelBd.Rating;
            ReviewText = reviewModelBd.ReviewText;
            IsAnonymous = reviewModelBd.isAnonymous;
            CreateDateTime = reviewModelBd.CreateDateTime;
            Author = new UserShortModel(reviewModelBd.User);
        }
    }
}
