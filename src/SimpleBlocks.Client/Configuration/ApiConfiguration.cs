namespace SimpleBlocks.Client.Configuration;

public class ApiConfiguration
{
    private readonly IConfiguration _config;

    public ApiConfiguration(IConfiguration config)
    {
        _config = config;
    }

    public string BaseUrl => _config["Api:BaseUrl"]!;
    public string AvailableLanguagesEndpoint => "api/AvailableLanguages";
    public string DownloadEndpoint => "api/Download";
    public string UploadEndpoint => "api/UploadLanguageFiles";
    public string ExecutionEndpoint => "api/ExecutionCode";
}