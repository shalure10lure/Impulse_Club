using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepo;
        private readonly IUserRepository _userRepo;

        public ClubService(IClubRepository clubRepo, IUserRepository userRepo)
        {
            _clubRepo = clubRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _clubRepo.GetAllAsync();
        }

        public async Task<Club?> GetOne(Guid id)
        {
            return await _clubRepo.GetByIdAsync(id);
        }

        public async Task<Club> CreateClub(CreateClubDto dto, Guid userId)
        {
            // Verify user exists and role
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Founder" && user.Role != "Admin")
                throw new UnauthorizedAccessException("Only founders and admins can create clubs");

            // Check if founder already has a club
            if (user.Role == "Founder")
            {
                var existingClub = await _clubRepo.GetByFundadorIdAsync(userId);
                if (existingClub != null)
                    throw new InvalidOperationException("Founders can only create one club");
            }

            var club = new Club
            {
                Name = dto.Name,
                SportType = dto.SportType,
                FounderId = userId
            };

            await _clubRepo.AddAsync(club);

            // Update user's FoundedClub
            user.FoundedClubId = club.Id;
            if (user.Role == "User")
                user.Role = "Founder"; // Promote to founder
            await _userRepo.UpdateAsync(user);

            return club;
        }

        public async Task<Club> UpdateClub(UpdateClubDto dto, Guid id, Guid userId)
        {
            var club = await _clubRepo.GetByIdAsync(id);
            if (club == null)
                throw new KeyNotFoundException("Club not found");

            // Verify permissions
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Admin" && club.FounderId != userId)
                throw new UnauthorizedAccessException("Only the club founder or admin can update this club");

            // Update fields
            if (!string.IsNullOrEmpty(dto.Name))
                club.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.SportType))
                club.SportType = dto.SportType;

            await _clubRepo.UpdateAsync(club);
            return club;
        }

        public async Task DeleteClub(Guid id)
        {
            var club = await _clubRepo.GetByIdAsync(id);
            if (club == null)
                throw new KeyNotFoundException("Club not found");

            await _clubRepo.DeleteAsync(club);
        }

        public async Task JoinClub(Guid clubId, Guid userId)
        {
            var club = await _clubRepo.GetByIdWithMiembrosAsync(clubId);
            if (club == null)
                throw new KeyNotFoundException("Club not found");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Check if user is already a member
            if (club.Members.Any(m => m.Id == userId))
                throw new InvalidOperationException("User is already a member of this club");

            club.Members.Add(user);
            await _clubRepo.UpdateAsync(club);
        }

        public async Task<IEnumerable<User>> GetClubMembers(Guid clubId)
        {
            var club = await _clubRepo.GetByIdWithMiembrosAsync(clubId);
            if (club == null)
                throw new KeyNotFoundException("Club not found");

            return club.Members;
        }
    }
}