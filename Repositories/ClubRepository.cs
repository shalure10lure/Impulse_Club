using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDbContext _db;
        public ClubRepository(AppDbContext db) { _db = db; }

        public async Task AddAsync(Club club)
        public ClubRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(Club club)
        {
            await _db.Clubes.AddAsync(club);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Club club)
        {
            _db.Clubes.Remove(club);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _db.Clubes.ToListAsync();
        }

        public async Task<Club> GetOne(Guid id)
        {
            return await _db.Clubes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Club club)
        {
            _db.Clubes.Update(club);
            await _db.SaveChangesAsync();
        }
    }
}
