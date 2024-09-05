using System.ComponentModel.DataAnnotations;

namespace EgyptWalks.API.DTOs
{
    public class WalkUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(1, 20)]
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

    }
}
