using System.Text.Json.Serialization;

namespace SimpleBlocks.Server.Infrastructure.Judge0.Models;

public record SubmissionResponse(
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("stdout")] string? Stdout,
    [property: JsonPropertyName("stderr")] string? Stderr,
    [property: JsonPropertyName("compile_output")] string? CompileOutput,
    [property: JsonPropertyName("message")] string? Message,
    [property: JsonPropertyName("status")] Status Status,
    [property: JsonPropertyName("time")] string? Time,
    [property: JsonPropertyName("memory")] double? Memory,
    [property: JsonPropertyName("compile_time")] string? CompileTime,
    [property: JsonPropertyName("execute_time")] string? ExecuteTime);

public record Status(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("description")] string Description); 