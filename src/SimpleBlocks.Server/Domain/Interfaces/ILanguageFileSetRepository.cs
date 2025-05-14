using SimpleBlocks.Server.Domain.Entities;

namespace SimpleBlocks.Server.Domain.Interfaces;

public interface ILanguageFileSetRepository
{
    Task<LanguageFileSet?> GetByLanguageKeyAsync(string languageKey, CancellationToken cancellationToken);
    Task AddAsync(LanguageFileSet fileSet, CancellationToken cancellationToken);
    Task UpdateAsync(LanguageFileSet fileSet, CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetAllLanguageKeysAsync(CancellationToken cancellationToken);
} 