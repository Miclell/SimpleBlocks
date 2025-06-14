using System.Net.Http.Json;
using SimpleBlocks.Client.Configuration;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Models;
using SimpleBlocks.Client.Helpers;

namespace SimpleBlocks.Client.Services;

public class FileService : BaseApiService
{
    private readonly LocalStorageService _localStorageService;

    public FileService(HttpClient httpClient, ApiConfiguration apiConfig, LocalStorageService localStorageService) 
        : base(httpClient, apiConfig)
    {
        _localStorageService = localStorageService;
    }

    public async Task<LanguageFiles?> GetLanguageFilesAsync(string language)
    {
        if (language.StartsWith("."))
        {
            // Загрузка из локального хранилища
            var localLanguages = await _localStorageService.GetLanguagesAsync();
            return localLanguages.FirstOrDefault(l => l.LanguageKey == language);
        }
        else
        {
            // Загрузка с сервера
            try
            {
                var dto = await GetAsync<LanguageFilesDto>($"{_apiConfig.DownloadEndpoint}/{language}");
                if (dto == null) return null;

                return new LanguageFiles
                {
                    LanguageKey = language,
                    BlocksJson = dto.Blocks,
                    SemanticsJson = dto.Semantics,
                    Source = LanguageSource.Server
                };
            }
            catch
            {
                return null;
            }
        }
    }

    public async Task<LanguageFilesDto?> UploadLanguageFilesAsync(string languageKey, string blocksJson, string semanticsJson)
    {
        var request = new { languageKey, blocksJson, semanticsJson };
        return await PostAsync<object, LanguageFilesDto>(_apiConfig.UploadEndpoint, request);
    }
} 