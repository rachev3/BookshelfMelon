    using MelonBookshelf.Models;
using Microsoft.EntityFrameworkCore;

namespace MelonBookshelf.Data.Services
{
    public class BackgroundTaskService : IBackgroundTaskService
    {
        private readonly ApplicationDbContext _appDbContext;

        public BackgroundTaskService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Add(BackgroundTask backgroundTask)
        {
            _appDbContext.BackgroundTasks.Add(backgroundTask);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var result = await _appDbContext.BackgroundTasks.FirstOrDefaultAsync(n => n.BackgroundTaskId == id);
            _appDbContext.BackgroundTasks.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
