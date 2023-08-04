using MelonBookshelf.Data;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Migrations;
using MelonBookshelf.Models.Email;
using MelonBookshelf.ReportGenerator;
using System.Text.Json;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace MelonBookshelf.BTask
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer? _timer = null;

        private List<BackgroundTask> _BackgroundTasks = new List<BackgroundTask>();
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _scopeFactory;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory  /*IBackgroundTaskService backgroundTaskService*/)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            //_backgroundTaskService = backgroundTaskService;
        }
        private async Task<List<BackgroundTask>> GetAndSortTasks(IBackgroundTaskService backgroundTaskService)
        {
            var tasks = await backgroundTaskService.GetAll();

            var sortedTasks = tasks.OrderBy(task => task.ExecutionTime).ToList();

            return sortedTasks;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            using (var scope = _scopeFactory.CreateScope())
            {
                var backgroundTaskService = scope.ServiceProvider.GetRequiredService<IBackgroundTaskService>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                _BackgroundTasks = await GetAndSortTasks(backgroundTaskService);

                foreach (var task in _BackgroundTasks)
                {
                    if (task.ExecutionTime < DateTime.UtcNow)
                    {
                        string json = task.Payload;

                        EmailJsonPayload emailJson = JsonSerializer.Deserialize<EmailJsonPayload>(json);
                        Message message = new Message();
                        message.Subject = emailJson.Subject;
                        message.Content = emailJson.Content;
                        message.ToEmails = emailJson.ToEmails;

                        message.Convert(message.ToEmails);

                        var filePath = emailJson.FilePathAttachments;
                        byte[] bytes = System.IO.File.ReadAllBytes(filePath);

                        using (var memoryStream = new MemoryStream(bytes))
                        {

                            var excelFile = new FormFile(memoryStream, 0, memoryStream.Length, "report.xlsx", "report.xlsx")
                            {
                                Headers = new HeaderDictionary(),
                                ContentType = "application/json",
                            };

                            var files = new List<IFormFile>();
                            files.Add(excelFile);

                            message.Attachments = files;

                            emailSender.SendEmail(message);
                            await backgroundTaskService.Remove(task.BackgroundTaskId);

                        }
                    }

                }

                _logger.LogInformation(
                    "Timed Hosted Service is working. Count: {Count}", count);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
