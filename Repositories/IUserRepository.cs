using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(Guid id);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAddress(string email);
        Task<User?> GetByRefreshToken(string refreshToken);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
