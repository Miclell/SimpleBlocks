using System.Net.Http.Json;
using SimpleBlocks.Client.Configuration;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Client.Services;

public class LanguageService : BaseApiService
{
    public LanguageService(HttpClient httpClient, ApiConfiguration apiConfig) 
        : base(httpClient, apiConfig)
    {
    }

    public async Task<IEnumerable<string>> GetAvailableLanguagesAsync()
    {
        return await GetAsync<IEnumerable<string>>(_apiConfig.AvailableLanguagesEndpoint) 
               ?? [];
    }
} 