using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IRecursoRepository
    {
        Task<IEnumerable<Recurso>> GetAll();
        Task<Recurso?> GetById(Guid id);
        Task AddAsync(Recurso recurso);
        Task UpdateAsync(Recurso recurso);
        Task DeleteAsync(Recurso recurso);
    }
}