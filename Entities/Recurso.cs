using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class Recurso
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // Cancha, Balón, Pesas...

        [Range(1, 10000)]
        public int CantidadTotal { get; set; } = 1;

        [Required]
        public string Estado { get; set; } = "Disponible"; // Disponible, En Uso, Mantenimiento

        [Required]
        public Guid ClubId { get; set; }
        public Club Club { get; set; } = default!; // 1:N

        public ICollection<Usuario> UsuariosQueReservaron { get; set; } = new List<Usuario>(); // N:M
    }
}
