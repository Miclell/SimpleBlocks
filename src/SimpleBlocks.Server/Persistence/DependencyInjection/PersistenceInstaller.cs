using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlocks.Server.Domain.Interfaces;
using SimpleBlocks.Server.Infrastructure.DependencyInjection;
using SimpleBlocks.Server.Persistence;
using SimpleBlocks.Server.Persistence.Configurations;
using SimpleBlocks.Server.Persistence.Repositories;
using SimpleBlocks.Server.Persistence.Seed.Interfaces;
using SimpleBlocks.Server.Persistence.Seed.Seeders;

namespace SimpleBlocks.Server.Persistence.DependencyInjection;

public class PersistenceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(DatabaseSettings.GetConnectionString(configuration),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        
        services.AddScoped<ISeeder, LanguageSeeder>();
        services.AddScoped<ILanguageFileSetRepository, LanguageFileSetRepository>();
    }
} 