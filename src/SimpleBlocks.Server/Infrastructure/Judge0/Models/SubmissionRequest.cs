using System.Text.Json.Serialization;

namespace SimpleBlocks.Server.Infrastructure.Judge0.Models;

public record SubmissionRequest(
    [property: JsonPropertyName("source_code")] string SourceCode,
    [property: JsonPropertyName("language_id")] int LanguageId,
    [property: JsonPropertyName("stdin")] string? Stdin = null); 