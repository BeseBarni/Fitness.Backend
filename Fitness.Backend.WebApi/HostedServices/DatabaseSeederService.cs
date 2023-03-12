using Fitness.Backend.Domain.Seeders;

namespace Fitness.Backend.WebApi.HostedServices
{
    public class DatabaseSeederService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeederService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var authSeeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();
                var lessonSeeder = scope.ServiceProvider.GetRequiredService<LessonSeeder>();

                await authSeeder.Initialize();
                await lessonSeeder.Initialize();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
