using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _repo;
        public ClubService(IClubRepository repo) { _repo = repo; }

        public async Task<Club> CreateClub(CreateClubDto dto)
        {
            var club = new Club
            {
                Nombre = dto.Nombre,
                TipoDeDeporte = dto.TipoDeDeporte,
                FundadorId = dto.FundadorId
            };
            await _repo.AddAsync(club);
            return club;
        }

        public async Task DeleteClub(Guid id)
        {
            var club = await _repo.GetById(id);
            if (club == null) return;
            await _repo.DeleteAsync(club);
        }

        public async Task<IEnumerable<Club>> GetAll() => await _repo.GetAll();

        public async Task<Club?> GetOne(Guid id) => await _repo.GetById(id);

        public async Task<Club> UpdateClub(UpdateClubDto dto, Guid id)
        {
            var club = await GetOne(id);
            if (club == null) throw new Exception("Club not found");

            if (!string.IsNullOrEmpty(dto.Nombre)) club.Nombre = dto.Nombre;
            if (!string.IsNullOrEmpty(dto.TipoDeDeporte)) club.TipoDeDeporte = dto.TipoDeDeporte;

            await _repo.UpdateAsync(club);
            return club;
        }
    }
}