using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlocks.Server.Application.Download;
using SimpleBlocks.Server.Application.Upload;
using SimpleBlocks.Server.Infrastructure.DependencyInjection;

namespace SimpleBlocks.Server.Application.DependencyInjection;

public class ApplicationInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationInstaller).Assembly));
        
        // Register validators
        services.AddValidatorsFromAssemblyContaining<UploadLanguageFilesValidator>();
        services.AddValidatorsFromAssemblyContaining<GetLanguageFilesValidator>();
    }
} 