namespace SimpleBlocks.Server.Domain.Entities;

public class LanguageFileSet
{
    public Guid Id { get; set; }
    public string LanguageKey { get; set; } = default!;
    public string BlocksJson { get; set; } = default!;
    public string SemanticsJson { get; set; } = default!;
}