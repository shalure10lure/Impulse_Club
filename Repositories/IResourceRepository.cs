using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetAllAsync();
        Task<Resource?> GetByIdAsync(Guid id);
        Task<IEnumerable<Resource>> GetByClubIdAsync(Guid clubId);
        Task AddAsync(Resource resource);
        Task UpdateAsync(Resource resource);
        Task DeleteAsync(Resource resource);
    }
}
