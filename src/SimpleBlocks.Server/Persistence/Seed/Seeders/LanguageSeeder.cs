using SimpleBlocks.Server.Domain.Entities;
using SimpleBlocks.Server.Persistence.Seed.Interfaces;
using System.IO;
using System.Linq;

namespace SimpleBlocks.Server.Persistence.Seed.Seeders;

public class LanguageSeeder : ISeeder
{
    private readonly string _basePath = Path.Combine("Persistence", "Seed", "Data");
    private readonly string[] _languagesNames = ["Brainfuck", "Csharp", "C++", "Dart", "Go", "Java", "JavaScript", "Lua", "PHP", "Python", "Rust"];

    public async Task SeedAsync(AppDbContext context)
    {
        if (context.LanguageFileSets.Any())
            return;

        var languageFileSets = new List<LanguageFileSet>();
        foreach (var language in _languagesNames)
            languageFileSets.Add(await GetLanguageFileSet(language));

        await context.LanguageFileSets.AddRangeAsync(languageFileSets);
        await context.SaveChangesAsync();
    }

    private async Task<LanguageFileSet> GetLanguageFileSet(string languageName)
    {
        var path = Path.Combine(_basePath, languageName);
        var blocksJsonPath = Path.Combine(path, "blocks.json");
        var semanticsJsonPath = Path.Combine(path, "semantics.json");
        
        if (!File.Exists(blocksJsonPath) || !File.Exists(semanticsJsonPath))
        {
            throw new FileNotFoundException("Не найдены необходимые файлы", path);
        }

        var blocksJson = await File.ReadAllTextAsync(blocksJsonPath);
        var semanticsJson = await File.ReadAllTextAsync(semanticsJsonPath);

        return new LanguageFileSet
        {
            Id = Guid.NewGuid(),
            LanguageKey = languageName,
            BlocksJson = blocksJson,
            SemanticsJson = semanticsJson
        };
    }
}