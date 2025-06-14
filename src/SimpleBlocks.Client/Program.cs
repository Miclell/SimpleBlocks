using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleBlocks.Client;
using SimpleBlocks.Client.Configuration;
using SimpleBlocks.Client.Services;

namespace SimpleBlocks.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        
        builder.Services.AddSingleton<ApiConfiguration>();

        builder.Services.AddScoped(sp => 
        {
            var apiConfig = sp.GetRequiredService<ApiConfiguration>();
            return new HttpClient { BaseAddress = new Uri(apiConfig.BaseUrl) };
        });
        
        builder.Services.AddScoped<FileService>();
        builder.Services.AddScoped<LanguageService>();
        builder.Services.AddScoped<ExecutionService>();
        builder.Services.AddScoped<ClipboardService>();
        builder.Services.AddScoped<LocalStorageService>();

        await builder.Build().RunAsync();
    }
}