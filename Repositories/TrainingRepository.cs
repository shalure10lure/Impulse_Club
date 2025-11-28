
using ImpulseClub.Data;
using ImpulseClub.Models;
using Microsoft.EntityFrameworkCore;

namespace ImpulseClub.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly AppDbContext _db;
        public TrainingRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task Add(Training training)
        {
            await _db.Trainings.AddAsync(training);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Training training)
        {
            _db.Trainings.Remove(training);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Training>> GetAll()
        {
            return await _db.Trainings.ToListAsync();
        }

        public async Task<Training> GetOne(Guid id)
        {
            return await _db.Trainings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Training training)
        {
            _db.Trainings.Update(training);
            await _db.SaveChangesAsync();
        }
    }
}
