using BackendDev.Data.Models;

namespace BackendDev.Data.ViewModels
{
    public class UserShortModel
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        public string? NickName { get; set; }
        public string? avatar { get; set; }

        public UserShortModel(string userId, string? nickName, string? avatar)
        {
            UserId = userId;
            NickName = nickName;
            this.avatar = avatar;
        }
        public UserShortModel (UserModel userModelBd)
        {
            UserId = userModelBd.Id.ToString();
            NickName = userModelBd.UserName;
            avatar = userModelBd.AvatarLink;
        }
    }
    
}
