using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.API.DTOs
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? Description { get; set; }
    }
}
