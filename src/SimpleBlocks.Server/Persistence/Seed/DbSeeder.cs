using SimpleBlocks.Server.Persistence.Seed.Interfaces;

namespace SimpleBlocks.Server.Persistence.Seed;

public static class DbSeeder
{
    public static async Task SeedAllAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var seeders = scope.ServiceProvider.GetServices<ISeeder>();

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(context);
        }
    }
}