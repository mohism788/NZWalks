using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTOs
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MaxLength(3, ErrorMessage ="Code must consist of 3 characters!")]
        [MinLength(3, ErrorMessage ="Code must consist of 3 characters!")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters!")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
