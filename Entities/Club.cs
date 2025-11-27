using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class Club
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string TipoDeDeporte { get; set; } = string.Empty;

        [Required]
        public Guid FundadorId { get; set; }
        public Usuario Fundador { get; set; } = default!; // 1:1

        // Relaciones
        public ICollection<Usuario> Miembros { get; set; } = new List<Usuario>(); // N:M
        public ICollection<Entrenamiento> Entrenamientos { get; set; } = new List<Entrenamiento>(); // 1:N
        public ICollection<Recurso> Recursos { get; set; } = new List<Recurso>(); // 1:N
    }
}
