using System.Text;

namespace SimpleBlocks.Shared.Dto.CodeExecution;

public record Response(
    string Output,
    string? Error,
    string? CompileOutput,
    string? Message,
    bool IsSuccess)
{
    public string DecodedOutput => DecodeBase64(Output);
    public string? DecodedError => DecodeBase64(Error);
    public string? DecodedCompileOutput => DecodeBase64(CompileOutput);
    public string? DecodedMessage => DecodeBase64(Message);

    private static string DecodeBase64(string? base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return string.Empty;
        
        try
        {
            var bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return base64String;
        }
    }
} 