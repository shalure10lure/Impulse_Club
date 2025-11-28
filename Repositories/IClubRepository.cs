using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<IEnumerable<Club>> GetAllAsync();
        Task<Club?> GetById(Guid id);
        Task<Club?> GetByIdAsync(Guid id);
        Task<Club?> GetByIdWithMiembrosAsync(Guid id);
        Task<Club?> GetByFundadorIdAsync(Guid fundadorId);
        Task<IEnumerable<Club>> GetClubesByUsuarioIdAsync(Guid usuarioId);
        Task AddAsync(Club club);
        Task UpdateAsync(Club club);
        Task DeleteAsync(Club club);
    }
}
