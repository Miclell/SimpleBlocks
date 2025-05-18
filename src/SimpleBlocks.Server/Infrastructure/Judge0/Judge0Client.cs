using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using SimpleBlocks.Server.Infrastructure.Judge0.Models;

namespace SimpleBlocks.Server.Infrastructure.Judge0;

public class Judge0Client : IJudge0Client
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<Judge0Client> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public Judge0Client(HttpClient httpClient, IOptions<Judge0Options> options, ILogger<Judge0Client> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
    }

    public async Task<SubmissionResponse> SubmitCodeAsync(SubmissionRequest request, CancellationToken cancellationToken = default)
    {
        var jsonRequest = JsonSerializer.Serialize(new
        {
            request.SourceCode,
            request.LanguageId,
            request.Stdin
        }, _jsonOptions);
        
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        _logger.LogInformation("Sending request to: {Uri}", new Uri(_httpClient.BaseAddress, "submissions?base64_encoded=true"));
        var response = await _httpClient.PostAsync("submissions?base64_encoded=true", content, cancellationToken);


        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Judge0 submission failed. Status: {Status}, Response: {Response}", 
                response.StatusCode, errorContent);
        }
        
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SubmissionResponse>(_jsonOptions, cancellationToken) 
               ?? throw new InvalidOperationException("Failed to deserialize submission response");
    }

    public async Task<SubmissionResponse> GetSubmissionAsync(string token, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"submissions/{token}?base64_encoded=true", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SubmissionResponse>(_jsonOptions, cancellationToken) 
               ?? throw new InvalidOperationException("Failed to deserialize submission response");
    }
} 