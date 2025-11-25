using ImpulseClub.Models;
using ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<Hospital>> GetAll();
        Task<Hospital> GetOne(Guid id);
        Task<Hospital> CreateHospital(CreateHospitalDto dto);
        Task<Hospital> UpdateHospital(UpdateHospitalDto dto, Guid id);
        Task DeleteHospital(Guid id);
    }
}
