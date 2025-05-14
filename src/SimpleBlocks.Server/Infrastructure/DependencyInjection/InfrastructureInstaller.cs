using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlocks.Server.Infrastructure.Judge0;

namespace SimpleBlocks.Server.Infrastructure.DependencyInjection;

public class InfrastructureInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Judge0Options>(options =>
        {
            options.BaseUrl = configuration["JUDGE0_URL"] ?? "http://localhost:2358";
        });
        services.AddHttpClient<IJudge0Client, Judge0Client>();
    }
} 