using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
namespace BackendDev.Data.Models
{
    public class ProfileModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
       public string? NickName { get; set; }
       public string Email { get; set; }
       public string? AvatarLink { get; set; }
       public string Name { get; set; }
       public string birthDate { get; set; }
     //  public Gender gender { get; set; }



}

}
