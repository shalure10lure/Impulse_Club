using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club?> GetOne(Guid id);
        Task<Club> CreateClub(CreateClubDto dto);
        Task<Club> UpdateClub(UpdateClubDto dto, Guid id);
        Task DeleteClub(Guid id);
    }
}