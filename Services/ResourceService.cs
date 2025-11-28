using ImpulseClub.Entities;
using ImpulseClub.Entities.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepo;
        private readonly IClubRepository _clubRepo;
        private readonly IUserRepository _userRepo;

        public ResourceService(
            IResourceRepository resourceRepo,
            IClubRepository clubRepo,
            IUserRepository userRepo)
        {
            _resourceRepo = resourceRepo;
            _clubRepo = clubRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Resource>> GetAll()
        {
            return await _resourceRepo.GetAllAsync();
        }

        public async Task<Resource?> GetOne(Guid id)
        {
            return await _resourceRepo.GetByIdAsync(id);
        }

        public async Task<Resource> CreateResource(CreateResourceDto dto, Guid userId)
        {
            // Verify club exists
            var club = await _clubRepo.GetByIdAsync(dto.ClubId);
            if (club == null)
                throw new KeyNotFoundException("Club not found");

            // Verify user is founder or admin
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Founder" && user.Role != "Admin")
                throw new UnauthorizedAccessException("Only founders and admins can create resources");

            // If founder, verify it's their club
            if (user.Role == "Founder" && club.FounderId != userId)
                throw new UnauthorizedAccessException("You can only create resources for your own club");

            var resource = new Resource
            {
                Name = dto.Name,
                Type = dto.Type,
                TotalQuantity = dto.TotalQuantity,
                Status = dto.Status,
                ClubId = dto.ClubId
            };

            await _resourceRepo.AddAsync(resource);
            return resource;
        }

        public async Task<Resource> UpdateResource(UpdateResourceDto dto, Guid id, Guid userId)
        {
            var resource = await _resourceRepo.GetByIdAsync(id);
            if (resource == null)
                throw new KeyNotFoundException("Resource not found");

            // Verify permissions
            var club = await _clubRepo.GetByIdAsync(resource.ClubId);
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Admin" && club?.FounderId != userId)
                throw new UnauthorizedAccessException("Only the club founder or admin can update this resource");

            // Update fields
            if (!string.IsNullOrEmpty(dto.Name))
                resource.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Type))
                resource.Type = dto.Type;

            if (dto.TotalQuantity.HasValue)
                resource.TotalQuantity = dto.TotalQuantity.Value;

            if (!string.IsNullOrEmpty(dto.Status))
                resource.Status = dto.Status;

            await _resourceRepo.UpdateAsync(resource);
            return resource;
        }

        public async Task DeleteResource(Guid id, Guid userId)
        {
            var resource = await _resourceRepo.GetByIdAsync(id);
            if (resource == null)
                throw new KeyNotFoundException("Resource not found");

            // Verify permissions
            var club = await _clubRepo.GetByIdAsync(resource.ClubId);
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Admin" && club?.FounderId != userId)
                throw new UnauthorizedAccessException("Only the club founder or admin can delete this resource");

            await _resourceRepo.DeleteAsync(resource);
        }

        public async Task ReserveResource(Guid resourceId, Guid userId, int quantity)
        {
            var resource = await _resourceRepo.GetByIdAsync(resourceId);
            if (resource == null)
                throw new KeyNotFoundException("Resource not found");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Check if user is member of the club or admin
            var club = await _clubRepo.GetByIdWithMiembrosAsync(resource.ClubId);
            if (club != null && !club.Members.Any(m => m.Id == userId) && user.Role != "Admin")
                throw new UnauthorizedAccessException("You must be a member of the club to reserve resources");

            // Check availability
            if (resource.Status != "Available")
                throw new InvalidOperationException($"Resource is currently {resource.Status}");

            if (quantity > resource.TotalQuantity)
                throw new InvalidOperationException($"Only {resource.TotalQuantity} units available");

            // Add reservation
            if (!resource.UsersWhoReserved.Any(u => u.Id == userId))
            {
                resource.UsersWhoReserved.Add(user);
                await _resourceRepo.UpdateAsync(resource);
            }
        }
    }
}
