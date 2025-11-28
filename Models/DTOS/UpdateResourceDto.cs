using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Models.DTOS
{
    public class UpdateResourceDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Type { get; set; }

        [Range(1, 10000)]
        public int? TotalQuantity { get; set; }

        public string? Status { get; set; } // Available, In Use, Maintenance
    }
}
