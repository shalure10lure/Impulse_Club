using System.ComponentModel.DataAnnotations;

        namespace ImpulseClub.Entities
{
    public class Club
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string SportType { get; set; } = string.Empty;

        [Required]
        public Guid FounderId { get; set; }
        public User Founder { get; set; } = default!; // 1:1

        // Relationships
        public ICollection<User> Members { get; set; } = new List<User>(); // N:M
        public ICollection<Training> Trainings { get; set; } = new List<Training>(); // 1:N
        public ICollection<Resource> Resources { get; set; } = new List<Resource>(); // 1:N
    }
}
