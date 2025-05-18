namespace SimpleBlocks.Shared.Dto.CodeExecution;

public record Request(
    string Code,
    int LanguageId,
    string? Stdin = null); 