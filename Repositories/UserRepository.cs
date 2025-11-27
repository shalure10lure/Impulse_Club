// ImpulseClub/Repositories/UserRepository.cs (ejemplo EF)
using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx) { _ctx = ctx; }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _ctx.Usuarios.ToListAsync();
        }
        public Task<Usuario?> GetById(Guid id) =>
            _ctx.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

        public Task<Usuario?> GetByEmailAddress(string email) =>
            _ctx.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        public Task<Usuario?> GetByRefreshToken(string refreshToken) =>
            _ctx.Usuarios.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        public async Task AddAsync(Usuario user)
        {
            _ctx.Usuarios.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario user)
        {
            _ctx.Usuarios.Update(user);
            await _ctx.SaveChangesAsync();
        }
        public async Task DeleteAsync(Usuario user)
        {
            _ctx.Usuarios.Remove(user);
            await _ctx.SaveChangesAsync();
        }
    }
}
