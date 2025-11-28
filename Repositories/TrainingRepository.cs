using ImpulseClub.Data;
using ImpulseClub.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImpulseClub.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly AppDbContext _context;

        public TrainingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Training>> GetAll()
        {
            return await _context.Trainings.Include(t => t.Club).ToListAsync();
        }

        public async Task<IEnumerable<Training>> GetAllAsync()
        {
            return await _context.Trainings
                .Include(t => t.Club)
                .ToListAsync();
        }

        public async Task<Training?> GetOne(Guid id)
        {
            return await _context.Trainings.Include(t => t.Club).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Training?> GetByIdAsync(Guid id)
        {
            return await _context.Trainings
                .Include(t => t.Club)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Training?> GetByIdWithParticipantsAsync(Guid id)
        {
            return await _context.Trainings
                .Include(t => t.Participants)
                .Include(t => t.Club)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Training>> GetByClubIdAsync(Guid clubId)
        {
            return await _context.Trainings
                .Where(t => t.ClubId == clubId)
                .Include(t => t.Club)
                .ToListAsync();
        }

        public async Task Add(Training training)
        {
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Training training)
        {
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Training training)
        {
            _context.Trainings.Update(training);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Training training)
        {
            _context.Trainings.Update(training);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Training training)
        {
            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Training training)
        {
            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
        }
    }
}
