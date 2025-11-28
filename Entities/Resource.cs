using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class Resource
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Type { get; set; } = string.Empty; // Court, Ball, Weights...

        [Range(1, 10000)]
        public int TotalQuantity { get; set; } = 1;

        [Required]
        public string Status { get; set; } = "Available"; // Available, In Use, Maintenance

        [Required]
        public Guid ClubId { get; set; }
        public Club Club { get; set; } = default!; // 1:N

        public ICollection<User> UsersWhoReserved { get; set; } = new List<User>(); // N:M
    }
}