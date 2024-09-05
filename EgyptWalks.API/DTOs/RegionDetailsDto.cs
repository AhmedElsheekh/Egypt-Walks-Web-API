namespace EgyptWalks.API.DTOs
{
    public class RegionDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ImageUrl { get; set; }
    }
}
