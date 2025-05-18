namespace SimpleBlocks.Server.Infrastructure.DependencyInjection;

public interface IServiceInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
} 