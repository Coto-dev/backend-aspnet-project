using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class ReviewShortModel
    {
        public string Id { get; set; }
        public int Rating { get; set; }

        public ReviewShortModel(string id, int rating)
        {
            Id = id;
            Rating = rating;
        }
        public ReviewShortModel(ReviewModelBd reviewModelBd)
        {
            Id = reviewModelBd.Id.ToString();
            Rating = reviewModelBd.Rating;
        }
        public ReviewShortModel()
        {

        }
    }
}
