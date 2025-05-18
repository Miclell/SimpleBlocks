using System.Text;
using SimpleBlocks.Client.Configuration;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Dto.CodeExecution;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Client.Services;

public class ExecutionService : BaseApiService
{
    public ExecutionService(HttpClient httpClient, ApiConfiguration apiConfig) 
        : base(httpClient, apiConfig)
    {
    }

    public async Task<Response?> ExecuteCodeAsync(string code, int languageId, string? stdin = null)
    {
        var encodedCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
        var encodedStdin = stdin != null ? Convert.ToBase64String(Encoding.UTF8.GetBytes(stdin)) : null;
        
        var request = new { code = encodedCode, languageId, stdin = encodedStdin };
        return await PostAsync<object, Response>($"{_apiConfig.ExecutionEndpoint}/execute", request);
    }
}