namespace BackendDev.Data.ViewModels
{
    public class PageInfoModel
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

        public PageInfoModel(int pageSize, int pageCount, int currentPage)
        {
            PageSize = pageSize;
            PageCount = pageCount;
            CurrentPage = currentPage;
        }
    }
}
