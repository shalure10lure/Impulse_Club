using Microsoft.EntityFrameworkCore;
using ImpulseClub.Data;
using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext _context;

        public ResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return await _context.Resources
                .Include(r => r.Club)
                .ToListAsync();
        }

        public async Task<Resource?> GetByIdAsync(Guid id)
        {
            return await _context.Resources
                .Include(r => r.Club)
                .Include(r => r.UsersWhoReserved)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Resource>> GetByClubIdAsync(Guid clubId)
        {
            return await _context.Resources
                .Where(r => r.ClubId == clubId)
                .ToListAsync();
        }

        public async Task AddAsync(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Resource resource)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Resource resource)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }
}
