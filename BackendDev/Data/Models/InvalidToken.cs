using System.ComponentModel.DataAnnotations;

namespace BackendDev.Data.Models
{
    public class InvalidToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string JWTToken { get; set; }
    }
}
