using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club?> GetById(Guid id);
        Task AddAsync(Club club);
        Task UpdateAsync(Club club);
        Task DeleteAsync(Club club);
    }
}
