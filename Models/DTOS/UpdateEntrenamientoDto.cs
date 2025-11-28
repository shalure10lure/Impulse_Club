using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Models.DTOS
{
    public record UpdateEntrenamientoDto
    {
        [StringLength(100)]
        public string? Nombre { get; init; }

        public DateTime? Fecha { get; init; }

        public int? Duracion { get; init; }
    }
}