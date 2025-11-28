using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;

namespace ImpulseClub.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepo;
        private readonly IClubRepository _clubRepo;
        private readonly IUserRepository _userRepo;

        public TrainingService(
            ITrainingRepository trainingRepo, 
            IClubRepository clubRepo,
            IUserRepository userRepo)
        {
            _trainingRepo = trainingRepo;
            _clubRepo = clubRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Training>> GetAll()
        {
            return await _trainingRepo.GetAllAsync();
        }

        public async Task<Training?> GetOne(Guid id)
        {
            return await _trainingRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Training>> GetTrainingsByClub(Guid clubId)
        {
            return await _trainingRepo.GetByClubIdAsync(clubId);
        }

        public async Task<Training> CreateTraining(CreateTrainingDto dto, Guid userId)
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
                throw new UnauthorizedAccessException("Only founders and admins can create trainings");

            // If founder, verify it's their club
            if (user.Role == "Founder" && club.FounderId != userId)
                throw new UnauthorizedAccessException("You can only create trainings for your own club");

            var training = new Training
            {
                Name = dto.Name,
                Date = dto.Date,
                Duration = dto.Duration,
                ClubId = dto.ClubId,
                UsedResources = dto.UsedResources
            };

            await _trainingRepo.AddAsync(training);
            return training;
        }

        public async Task<Training> UpdateTraining(UpdateTrainingDto dto, Guid id, Guid userId)
        {
            var training = await _trainingRepo.GetByIdAsync(id);
            if (training == null)
                throw new KeyNotFoundException("Training not found");

            // Verify permissions
            var club = await _clubRepo.GetByIdAsync(training.ClubId);
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Admin" && club?.FounderId != userId)
                throw new UnauthorizedAccessException("Only the club founder or admin can update this training");

            // Update fields
            if (!string.IsNullOrEmpty(dto.Name))
                training.Name = dto.Name;

            if (dto.Date.HasValue)
                training.Date = dto.Date.Value;

            if (dto.Duration.HasValue)
                training.Duration = dto.Duration.Value;

            if (!string.IsNullOrEmpty(dto.UsedResources))
                training.UsedResources = dto.UsedResources;

            await _trainingRepo.UpdateAsync(training);
            return training;
        }

        public async Task DeleteTraining(Guid id, Guid userId)
        {
            var training = await _trainingRepo.GetByIdAsync(id);
            if (training == null)
                throw new KeyNotFoundException("Training not found");

            // Verify permissions
            var club = await _clubRepo.GetByIdAsync(training.ClubId);
            var user = await _userRepo.GetByIdAsync(userId);

            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (user.Role != "Admin" && club?.FounderId != userId)
                throw new UnauthorizedAccessException("Only the club founder or admin can delete this training");

            await _trainingRepo.DeleteAsync(training);
        }

        public async Task JoinTraining(Guid trainingId, Guid userId)
        {
            var training = await _trainingRepo.GetByIdWithParticipantsAsync(trainingId);
            if (training == null)
                throw new KeyNotFoundException("Training not found");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            // Check if user is already a participant
            if (training.Participants.Any(p => p.Id == userId))
                throw new InvalidOperationException("User is already registered for this training");

            // Check if user is member of the club
            var club = await _clubRepo.GetByIdWithMiembrosAsync(training.ClubId);
            if (club != null && !club.Members.Any(m => m.Id == userId) && user.Role != "Admin")
                throw new UnauthorizedAccessException("You must be a member of the club to join this training");

            training.Participants.Add(user);
            await _trainingRepo.UpdateAsync(training);
        }
    }
}
