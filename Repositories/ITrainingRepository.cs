namespace ImpulseClub.Repositories
{
    public interface ITrainingRepository
    {
        Task<IEnumerable<Training>> GetAll();
        Task<Training> GetOne(Guid id);
        Task Add(Training training);
        Task Update(Training training);
        Task Delete(Training training);
    }
}
