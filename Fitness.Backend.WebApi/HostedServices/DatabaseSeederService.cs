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
                var seeder = scope.ServiceProvider.GetRequiredService<AuthSeeder>();

                await seeder.Initialize();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
