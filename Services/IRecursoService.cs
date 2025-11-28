using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IRecursoService
    {
        Task<IEnumerable<Recurso>> GetAll();
        Task<Recurso?> GetOne(Guid id);
        Task<Recurso> CreateRecurso(CreateRecursoDto dto);
        Task<Recurso> UpdateRecurso(UpdateRecursoDto dto, Guid id);
        Task DeleteRecurso(Guid id);
    }
}