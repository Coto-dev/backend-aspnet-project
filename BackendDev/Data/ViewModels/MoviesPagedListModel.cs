namespace BackendDev.Data.ViewModels
{
    public class MoviesPagedListModel
    {
        public List<MovieElementModel>? Movies { get; set; }
        public PageInfoModel PageInfo { get; set; }
        public MoviesPagedListModel(List<MovieElementModel>? movies, PageInfoModel pageInfo)
        {
            Movies = movies;
            PageInfo = pageInfo;
        }
       /* public MoviesPagedListModel(List<MovieElementModel> movies, PageInfoModel pageInfo)
        {
            Movies = movies;//tolist
            PageInfo = pageInfo;
        }*/
    }
}
