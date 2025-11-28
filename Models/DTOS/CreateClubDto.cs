using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Models.DTOS
{
    public record CreateClubDto
    {
        [Required]
        public string Nombre { get; init; }
        [Required]
        public string TipoDeDeporte { get; init; }
        [Required]
        public Guid FundadorId { get; init; }
    }
}