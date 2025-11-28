using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Models.DTOS
{
    public record UpdateClubDto
    {
        [StringLength(100)]
        public string? Nombre { get; init; }

        [StringLength(50)]
        public string? TipoDeDeporte { get; init; }
    }
}