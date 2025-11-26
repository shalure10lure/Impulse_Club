using ImpulseClub.Models;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IUserService
    {
       // Task<IEnumerable<User>> GetAll();
        Task<User> GetOne(Guid id);
        Task<User> UpdateUser(UpdateUserDto dto, Guid id);
        Task DeleteUser(Guid id);
    }
}
