using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public class CreateTrainingDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 480)] // Max 8 hours
        public int Duration { get; set; } // Minutes

        [Required]
        public Guid ClubId { get; set; }

        public string UsedResources { get; set; } = "[]"; // JSON format
    }
}
