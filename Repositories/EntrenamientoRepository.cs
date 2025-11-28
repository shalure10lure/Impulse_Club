using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public class EntrenamientoRepository : IEntrenamientoRepository
    {
        private readonly AppDbContext _db;
        public EntrenamientoRepository(AppDbContext db) { _db = db; }

        public async Task AddAsync(Entrenamiento entrenamiento)
        {
            await _db.Entrenamientos.AddAsync(entrenamiento);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Entrenamiento entrenamiento)
        {
            _db.Entrenamientos.Remove(entrenamiento);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entrenamiento>> GetAll()
        {
            return await _db.Entrenamientos.Include(e => e.Participantes).ToListAsync();
        }

        public async Task<Entrenamiento?> GetById(Guid id)
        {
            return await _db.Entrenamientos.Include(e => e.Participantes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Entrenamiento entrenamiento)
        {
            _db.Entrenamientos.Update(entrenamiento);
            await _db.SaveChangesAsync();
        }
    }
}