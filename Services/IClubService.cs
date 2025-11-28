using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club?> GetOne(Guid id);
        Task<Club> CreateClub(CreateClubDto dto, Guid userId);
        Task<Club> UpdateClub(UpdateClubDto dto, Guid id, Guid userId);
        Task DeleteClub(Guid id);
        Task JoinClub(Guid clubId, Guid userId);
        Task<IEnumerable<User>> GetClubMembers(Guid clubId);
    }
}