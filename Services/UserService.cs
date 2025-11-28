using ImpulseClub.Entities;
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

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Usuario?> GetOne(Guid id)
        {
            return await _repo.GetById(id);
        }

        public async Task<Usuario> UpdateUser(UpdateUserDto dto, Guid id)
        {
            Usuario? user = await GetOne(id);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            if (!string.IsNullOrEmpty(dto.Name))
                user.Nombre = dto.Name;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _repo.UpdateAsync(user);

            return user;
        }

        public async Task DeleteUser(Guid id)
        {
            Usuario? user = (await GetAll()).FirstOrDefault(h => h.Id == id);
            if (user == null) return;
            await _repo.DeleteAsync(user);
        }
    }
}
