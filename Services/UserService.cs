using ImpulseClub.Models;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<User?> GetOne(Guid id)
        {
            return await _repo.GetById(id);
        }

        public async Task<User> UpdateUser(UpdateUserDto dto, Guid id)
        {
            User? user = await GetOne(id);
            if (user == null) throw new Exception("User doesnt exist.");

            user.Username = dto.Name;
            user.Email = dto.Email;
            user.PasswordHash = dto.Password;

            await _repo.UpdateAsync(user);
            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            User? user = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (user == null) return;
            await _repo.DeleteAsync(user);
        }
    }
}
