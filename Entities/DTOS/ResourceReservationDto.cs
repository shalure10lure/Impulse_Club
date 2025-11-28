using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public class ResourceReservationDto
    {
        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; } = 1;
    }
}
