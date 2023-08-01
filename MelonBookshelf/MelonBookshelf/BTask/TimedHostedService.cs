using MelonBookshelf.Data;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Migrations;
using MelonBookshelf.Models.Email;
using System.Text.Json;

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
                        List<string> strings = JsonSerializer.Deserialize<List<string>>(json);
                        Message message = JsonSerializer.Deserialize<Message>(strings[0]);
                        message.Convert(message.ToEmails);

                        emailSender.SendEmail(message);
                        backgroundTaskService.Remove(task.BackgroundTaskId);

                    }
                }

                //var messageComponents = BackgroundTasks[0].Payload.Split(',').ToList();


                //List<string> emails = new List<string>();
                //var email = messageComponents[0];
                //emails.Add(email);

                //var message = new Message(emails, messageComponents[1], messageComponents[2]);
                //emailSender.SendEmail(message);

            }

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
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
