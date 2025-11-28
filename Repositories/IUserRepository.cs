using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario?> GetById(Guid id);
        Task<Usuario?> GetByEmailAddress(string email);
        Task<Usuario?> GetByRefreshToken(string refreshToken);
        Task AddAsync(Usuario user);
        Task UpdateAsync(Usuario user);
        Task DeleteAsync(Usuario user);
    }
}
