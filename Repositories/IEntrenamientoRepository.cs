using ImpulseClub.Entities;

namespace ImpulseClub.Repositories
{
    public interface IEntrenamientoRepository
    {
        Task<IEnumerable<Entrenamiento>> GetAll();
        Task<Entrenamiento?> GetById(Guid id);
        Task AddAsync(Entrenamiento entrenamiento);
        Task UpdateAsync(Entrenamiento entrenamiento);
        Task DeleteAsync(Entrenamiento entrenamiento);
    }
}