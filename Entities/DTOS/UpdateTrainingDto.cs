using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public class UpdateTrainingDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        public DateTime? Date { get; set; }

        [Range(1, 480)]
        public int? Duration { get; set; }

        public string? UsedResources { get; set; } // JSON format
    }
}
