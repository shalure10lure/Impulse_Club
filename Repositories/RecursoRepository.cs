using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public class RecursoRepository : IRecursoRepository
    {
        private readonly AppDbContext _db;
        public RecursoRepository(AppDbContext db) { _db = db; }

        public async Task AddAsync(Recurso recurso)
        {
            await _db.Recursos.AddAsync(recurso);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Recurso recurso)
        {
            _db.Recursos.Remove(recurso);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recurso>> GetAll()
        {
            return await _db.Recursos.Include(r => r.UsuariosQueReservaron).ToListAsync();
        }

        public async Task<Recurso?> GetById(Guid id)
        {
            return await _db.Recursos.Include(r => r.UsuariosQueReservaron).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Recurso recurso)
        {
            _db.Recursos.Update(recurso);
            await _db.SaveChangesAsync();
        }
    }
}