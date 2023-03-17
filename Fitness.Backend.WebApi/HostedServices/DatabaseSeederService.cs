using Fitness.Backend.Application.Seeders;

namespace Fitness.Backend.WebApi.HostedServices
{
    /// <summary>
    /// Middleware service for initializing database tables and seeding them with data
    /// Runs the first the application is started
    /// </summary>
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
                var lessonSeeder = scope.ServiceProvider.GetRequiredService<AppDataSeeder>();

                await authSeeder.Initialize();
                await lessonSeeder.Initialize();
                Console.WriteLine("Init finished");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
