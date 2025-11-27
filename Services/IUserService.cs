using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario?> GetOne(Guid id);
        Task<Usuario> UpdateUser(UpdateUserDto dto, Guid id);
        Task DeleteUser(Guid id);
    }
}
