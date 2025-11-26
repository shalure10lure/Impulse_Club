using System.ComponentModel.DataAnnotations;

namespace ImpulseClub.Models.DTOS
{
    public class UpdateUserDto
    {
        [StringLength(200)]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // Opcional: si el usuario quiere cambiar su contraseña
        [StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
