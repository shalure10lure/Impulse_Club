using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Entities.DTOS
{
    public class UpdateClubDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? SportType { get; set; }
    }
}