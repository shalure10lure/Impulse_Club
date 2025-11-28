using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public record CreateRecursoDto
    {
        [Required]
        public string Nombre { get; init; }

        [Required]
        public string Tipo { get; init; }

        [Range(1, 10000)]
        public int CantidadTotal { get; init; }

        [Required]
        public Guid ClubId { get; init; }
    }
}