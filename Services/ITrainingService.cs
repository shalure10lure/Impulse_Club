using ImpulseClub.Entities;
using ImpulseClub.Entities.DTOS;

namespace ImpulseClub.Services
{
    public interface ITrainingService
    {
        Task<IEnumerable<Training>> GetAll();
        Task<Training?> GetOne(Guid id);
        Task<IEnumerable<Training>> GetTrainingsByClub(Guid clubId);
        Task<Training> CreateTraining(CreateTrainingDto dto, Guid userId);
        Task<Training> UpdateTraining(UpdateTrainingDto dto, Guid id, Guid userId);
        Task DeleteTraining(Guid id, Guid userId);
        Task JoinTraining(Guid trainingId, Guid userId);
    }
}
