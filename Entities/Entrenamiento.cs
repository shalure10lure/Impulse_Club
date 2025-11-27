using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class Entrenamiento
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int Duracion { get; set; } // Minutos

        [Required]
        public Guid ClubId { get; set; }
        public Club Club { get; set; } = default!; // 1:N

        public ICollection<Usuario> Participantes { get; set; } = new List<Usuario>(); // N:M

        // Recursos usados en formato JSON (simplificado sin tabla intermedia)
        public string RecursosUsados { get; set; } = "[]"; // JSON: [{"RecursoId": "...", "Cantidad": 10}]
    }
}
