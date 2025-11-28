using ImpulseClub.Entities;
using ImpulseClub.Entities.DTOS;

namespace ImpulseClub.Services
{
    public interface IResourceService
    {
        Task<IEnumerable<Resource>> GetAll();
        Task<Resource?> GetOne(Guid id);
        Task<Resource> CreateResource(CreateResourceDto dto, Guid userId);
        Task<Resource> UpdateResource(UpdateResourceDto dto, Guid id, Guid userId);
        Task DeleteResource(Guid id, Guid userId);
        Task ReserveResource(Guid resourceId, Guid userId, int quantity);
    }
}
