using SimpleBlocks.Client.Configuration;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Client.Services;

public class FileService : BaseApiService
{
    public FileService(HttpClient httpClient, ApiConfiguration apiConfig) 
        : base(httpClient, apiConfig)
    {
    }

    public async Task<LanguageFilesDto?> GetLanguageFilesAsync(string languageKey)
    {
        return await GetAsync<LanguageFilesDto>($"{_apiConfig.DownloadEndpoint}/{languageKey}");
    }

    public async Task<LanguageFilesDto?> UploadLanguageFilesAsync(string languageKey, string blocksJson, string semanticsJson)
    {
        var request = new { languageKey, blocksJson, semanticsJson };
        return await PostAsync<object, LanguageFilesDto>(_apiConfig.UploadEndpoint, request);
    }
} 