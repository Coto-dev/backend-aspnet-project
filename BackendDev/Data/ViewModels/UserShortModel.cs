namespace BackendDev.Data.ViewModels
{
    public class UserShortModel
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string? NickName { get; set; }
        public string? avatar { get; set; }
    }
}
