using Microsoft.EntityFrameworkCore;
using SimpleBlocks.Server.Domain.Entities;
using SimpleBlocks.Server.Domain.Interfaces;
using SimpleBlocks.Server.Persistence;

namespace SimpleBlocks.Server.Persistence.Repositories;

public class LanguageFileSetRepository : ILanguageFileSetRepository
{
    private readonly AppDbContext _db;

    public LanguageFileSetRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<LanguageFileSet?> GetByLanguageKeyAsync(string languageKey, CancellationToken cancellationToken)
    {
        return await _db.LanguageFileSets
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LanguageKey == languageKey, cancellationToken);
    }

    public async Task AddAsync(LanguageFileSet fileSet, CancellationToken cancellationToken)
    {
        await _db.LanguageFileSets.AddAsync(fileSet, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LanguageFileSet fileSet, CancellationToken cancellationToken)
    {
        _db.LanguageFileSets.Update(fileSet);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetAllLanguageKeysAsync(CancellationToken cancellationToken)
    {
        return await _db.LanguageFileSets
            .AsNoTracking()
            .Select(x => x.LanguageKey)
            .ToListAsync(cancellationToken);
    }
} 