namespace BackendDev.Data.ViewModels
{
    public class MovieElementModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Poster { get; set; }
        public int Year { get; set; }
        public string? Country { get; set; }
        public GenreModelDTO? Genres { get; set; }
        public ReviewShortModel? Reviews { get; set; }

    }
}
