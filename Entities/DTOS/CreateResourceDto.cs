using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public class CreateResourceDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000)]
        public int TotalQuantity { get; set; } = 1;

        [Required]
        public Guid ClubId { get; set; }

        public string Status { get; set; } = "Available"; // Available, In Use, Maintenance
    }
}
