using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTOs
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength (20, ErrorMessage ="Name is too long")]
        public string Name { get; set; }

        [Required]
        [MaxLength (500, ErrorMessage = "Discription is too long")]
        [MinLength (10, ErrorMessage = "Discription is too short")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Distance must be less than (50 km)")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
