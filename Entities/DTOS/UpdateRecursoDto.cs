using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public record UpdateRecursoDto
    {
        [StringLength(100)]
        public string? Nombre { get; init; }

        [StringLength(50)]
        public string? Tipo { get; init; }

        public int? CantidadTotal { get; init; }

        public string? Estado { get; init; }
    }
}