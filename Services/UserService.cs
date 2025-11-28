using ImpulseClub.Entities;
using ImpulseClub.Entities.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class UserService : IUserService
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
            if (user == null)
                throw new KeyNotFoundException("User not found");

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _repo.UpdateAsync(user);

            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            User? user = (await GetAll()).FirstOrDefault(u => u.Id == id);
            if (user == null) return;
            await _repo.DeleteAsync(user);
        }
    }
}
