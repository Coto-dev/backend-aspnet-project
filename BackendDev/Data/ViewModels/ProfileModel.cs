using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class ProfileModel
    {
        public string Id { get; set; }
        public string? NickName { get; set; }
        public string Email { get; set; }
        public string? AvatarLink { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public Gender Gender  { get; set; }

    }
}
