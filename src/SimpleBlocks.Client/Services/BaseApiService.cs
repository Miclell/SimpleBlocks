using System.Net.Http.Json;
using SimpleBlocks.Client.Configuration;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Client.Services;

public abstract class BaseApiService
{
    protected readonly HttpClient _httpClient;
    protected readonly ApiConfiguration _apiConfig;

    protected BaseApiService(HttpClient httpClient, ApiConfiguration apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig;
    }

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<T>>(endpoint);
            if (response != null) return response.ResponseData;
            Console.WriteLine($"Сервер вернул null при GET запросе к {endpoint}");
            return default;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Ошибка при выполнении GET запроса к {endpoint}: {ex.Message}");
            return default;
        }
    }

    protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TResponse>>();
            if (result != null) return result.ResponseData;
            Console.WriteLine($"Сервер вернул null при POST запросе к {endpoint}");
            return default;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Ошибка при выполнении POST запроса к {endpoint}: {ex.Message}");
            return default;
        }
    }
} 