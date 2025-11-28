using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities
{
    public class Training
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; } // Minutes

        [Required]
        public Guid ClubId { get; set; }
        public Club Club { get; set; } = default!; // 1:N

        public ICollection<User> Participants { get; set; } = new List<User>(); // N:M

        // Resources used in JSON format (simplified without intermediate table)
        public string UsedResources { get; set; } = "[]"; // JSON: [{"ResourceId": "...", "Quantity": 10}]
    }
}