using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDbContext _context;

        public ClubRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.Include(c => c.Founder).ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetAllAsync()
        {
            return await _context.Clubs
                .Include(c => c.Founder)
                .ToListAsync();
        }

        public async Task<Club?> GetById(Guid id)
        {
            return await _context.Clubs
                .Include(c => c.Founder)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club?> GetByIdAsync(Guid id)
        {
            return await _context.Clubs
                .Include(c => c.Founder)
                .Include(c => c.Resources)
                .Include(c => c.Trainings)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club?> GetByIdWithMiembrosAsync(Guid id)
        {
            return await _context.Clubs
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Club?> GetByFundadorIdAsync(Guid fundadorId)
        {
            return await _context.Clubs.FirstOrDefaultAsync(c => c.FounderId == fundadorId);
        }

        public async Task<IEnumerable<Club>> GetClubesByUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Clubs
                .Where(c => c.Members.Any(m => m.Id == usuarioId))
                .ToListAsync();
        }

        public async Task AddAsync(Club club)
        {
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Club club)
        {
            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Club club)
        {
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
        }
    }
}
