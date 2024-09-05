using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
