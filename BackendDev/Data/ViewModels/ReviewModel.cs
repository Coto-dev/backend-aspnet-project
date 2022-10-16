namespace BackendDev.Data.ViewModels
{
    public class ReviewModel
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public bool IsAnonymous { get; set; }
        public string CreateDateTime { get; set; }
        public UserShortModel Author { get; set; }
    }
}
