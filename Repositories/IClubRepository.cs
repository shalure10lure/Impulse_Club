using ImpulseClub.Models;

namespace ImpulseClub.Repositories
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetOne(Guid id);
        Task Add(Club club);
        Task Update(Club club);
        Task Delete(Club club);
    }
}
