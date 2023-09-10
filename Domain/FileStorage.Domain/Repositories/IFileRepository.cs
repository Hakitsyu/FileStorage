namespace FileStorage.Domain.Repositories
{
    public interface IFileRepository
    {
        Task AddAsync(Entities.File file);
        Task UpdateAsync(Entities.File file);
        Task<Entities.File?> GetByIdAsync(Guid id);
    }
}
