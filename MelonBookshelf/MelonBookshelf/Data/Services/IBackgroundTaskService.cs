using MelonBookshelf.Models;
namespace MelonBookshelf.Data.Services

{
    public interface IBackgroundTaskService
    {
        Task Add(BackgroundTask backgroundTask);
        Task<List<BackgroundTask>> GetAll();
        Task Remove(int id);
    }
}
