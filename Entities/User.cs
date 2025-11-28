using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class User
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "User"; // Admin, User, Founder

        // JWT Refresh Token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenRevokedAt { get; set; }
        public string? CurrentJwtId { get; set; }

        // Relationships
        public Club? FoundedClub { get; set; } // 1:1 - only if Founder
        public Guid? FoundedClubId { get; set; }

        public ICollection<Club> Clubs { get; set; } = new List<Club>(); // N:M - member of multiple clubs
        public ICollection<Training> Trainings { get; set; } = new List<Training>(); // N:M - participates in trainings
        public ICollection<Resource> ReservedResources { get; set; } = new List<Resource>(); // N:M - reserved resources
    }
}