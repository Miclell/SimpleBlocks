using System.Text.Json;
using Microsoft.JSInterop;
using SimpleBlocks.Client.Helpers;

namespace SimpleBlocks.Client.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string LanguagesKey = "userLanguages";

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SaveLanguageAsync(LanguageFiles language)
        {
            var languages = await GetLanguagesAsync();
            languages.Add(language);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", LanguagesKey, JsonSerializer.Serialize(languages));
        }

        public async Task<List<LanguageFiles>> GetLanguagesAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", LanguagesKey);
            if (string.IsNullOrEmpty(json))
                return new List<LanguageFiles>();

            return JsonSerializer.Deserialize<List<LanguageFiles>>(json) ?? new List<LanguageFiles>();
        }
    }
} 