using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.API.DTOs
{
    public class RegionCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(10, ErrorMessage = "Name should be of minimum 10 characters")]
        [MaxLength(100, ErrorMessage = "Name should be of maximum 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [MinLength(3, ErrorMessage = "Code should be of minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "Code should be of maximum 3 characters")]
        public string Code { get; set; }
        public string? ImageUrl { get; set; }
    }
}
