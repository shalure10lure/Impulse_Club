using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class RecursoService : IRecursoService
    {
        private readonly IRecursoRepository _repo;
        public RecursoService(IRecursoRepository repo) { _repo = repo; }

        public async Task<Recurso> CreateRecurso(CreateRecursoDto dto)
        {
            var r = new Recurso
            {
                Nombre = dto.Nombre,
                Tipo = dto.Tipo,
                CantidadTotal = dto.CantidadTotal,
                ClubId = dto.ClubId
            };
            await _repo.AddAsync(r);
            return r;
        }

        public async Task DeleteRecurso(Guid id)
        {
            var r = await _repo.GetById(id);
            if (r == null) return;
            await _repo.DeleteAsync(r);
        }

        public async Task<IEnumerable<Recurso>> GetAll() => await _repo.GetAll();

        public async Task<Recurso?> GetOne(Guid id) => await _repo.GetById(id);

        public async Task<Recurso> UpdateRecurso(UpdateRecursoDto dto, Guid id)
        {
            var r = await GetOne(id);
            if (r == null) throw new Exception("Recurso not found");

            if (!string.IsNullOrEmpty(dto.Nombre)) r.Nombre = dto.Nombre;
            if (!string.IsNullOrEmpty(dto.Tipo)) r.Tipo = dto.Tipo;
            if (dto.CantidadTotal.HasValue) r.CantidadTotal = dto.CantidadTotal.Value;
            if (!string.IsNullOrEmpty(dto.Estado)) r.Estado = dto.Estado;

            await _repo.UpdateAsync(r);
            return r;
        }
    }
}