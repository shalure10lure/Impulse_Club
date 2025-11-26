// ImpulseClub/Repositories/UserRepository.cs (ejemplo EF)
using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Models;

namespace ImpulseClub.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx) { _ctx = ctx; }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _ctx.Users.ToListAsync();
        }
        public Task<User?> GetById(Guid id) =>
            _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);

        public Task<User?> GetByEmailAddress(string email) =>
            _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task<User?> GetByRefreshToken(string refreshToken) =>
            _ctx.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        public async Task AddAsync(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _ctx.Users.Update(user);
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            _ctx.Users.Remove(user);
            await _ctx.SaveChangesAsync();
        }
    }
}
