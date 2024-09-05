using EgyptWalks.Core.Models.Domain;

namespace EgyptWalks.API.DTOs
{
    public class WalkDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }
        public RegionDetailsDto RegionDetails { get; set; }
        public DifficultyDetailsDto DifficultyDetails { get; set; }
    }
}
