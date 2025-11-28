using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IEntrenamientoService
    {
        Task<IEnumerable<Entrenamiento>> GetAll();
        Task<Entrenamiento?> GetOne(Guid id);
        Task<Entrenamiento> CreateEntrenamiento(CreateEntrenamientoDto dto);
        Task<Entrenamiento> UpdateEntrenamiento(UpdateEntrenamientoDto dto, Guid id);
        Task DeleteEntrenamiento(Guid id);
    }
}