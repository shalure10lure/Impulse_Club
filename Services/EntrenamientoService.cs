using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class EntrenamientoService : IEntrenamientoService
    {
        private readonly IEntrenamientoRepository _repo;
        public EntrenamientoService(IEntrenamientoRepository repo) { _repo = repo; }

        public async Task<Entrenamiento> CreateEntrenamiento(CreateEntrenamientoDto dto)
        {
            var ent = new Entrenamiento
            {
                Nombre = dto.Nombre,
                Fecha = dto.Fecha,
                Duracion = dto.Duracion,
                ClubId = dto.ClubId
            };
            await _repo.AddAsync(ent);
            return ent;
        }

        public async Task DeleteEntrenamiento(Guid id)
        {
            var ent = await _repo.GetById(id);
            if (ent == null) return;
            await _repo.DeleteAsync(ent);
        }

        public async Task<IEnumerable<Entrenamiento>> GetAll() => await _repo.GetAll();

        public async Task<Entrenamiento?> GetOne(Guid id) => await _repo.GetById(id);

        public async Task<Entrenamiento> UpdateEntrenamiento(UpdateEntrenamientoDto dto, Guid id)
        {
            var ent = await GetOne(id);
            if (ent == null) throw new Exception("Entrenamiento not found");

            if (!string.IsNullOrEmpty(dto.Nombre)) ent.Nombre = dto.Nombre;
            if (dto.Fecha.HasValue) ent.Fecha = dto.Fecha.Value;
            if (dto.Duracion.HasValue) ent.Duracion = dto.Duracion.Value;

            await _repo.UpdateAsync(ent);
            return ent;
        }
    }
}