using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class Usuario
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = "User"; // Admin, User, Fundador

        // JWT Refresh Token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenRevokedAt { get; set; }
        public string? CurrentJwtId { get; set; }

        // Relaciones
        public Club? ClubFundado { get; set; } // 1:1 - solo si es Fundador
        public Guid? ClubFundadoId { get; set; }

        public ICollection<Club> Clubes { get; set; } = new List<Club>(); // N:M - miembro de varios clubes
        public ICollection<Entrenamiento> Entrenamientos { get; set; } = new List<Entrenamiento>(); // N:M - participa en entrenamientos
        public ICollection<Recurso> RecursosReservados { get; set; } = new List<Recurso>(); // N:M - recursos reservados
    }
}