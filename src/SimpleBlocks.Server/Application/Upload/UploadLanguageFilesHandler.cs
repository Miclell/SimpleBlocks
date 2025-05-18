using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Domain.Entities;
using SimpleBlocks.Server.Domain.Interfaces;

namespace SimpleBlocks.Server.Application.Upload;

public class UploadLanguageFilesHandler : IRequestHandler<UploadLanguageFilesCommand, Result<LanguageFileSet>>
{
    private readonly ILanguageFileSetRepository _repository;

    public UploadLanguageFilesHandler(ILanguageFileSetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<LanguageFileSet>> Handle(UploadLanguageFilesCommand request, CancellationToken cancellationToken)
    {
        var existingSet = await _repository.GetByLanguageKeyAsync(request.LanguageKey, cancellationToken);
        if (existingSet != null)
        {
            UpdateExistingSet(existingSet, request.BlocksJson, request.SemanticsJson);
            await _repository.UpdateAsync(existingSet, cancellationToken);
            return Result<LanguageFileSet>.Success(existingSet);
        }

        var newSet = CreateNewSet(request.LanguageKey, request.BlocksJson, request.SemanticsJson);
        await _repository.AddAsync(newSet, cancellationToken);
        return Result<LanguageFileSet>.Success(newSet);
    }

    private static void UpdateExistingSet(LanguageFileSet set, string blocksJson, string semanticsJson)
    {
        set.BlocksJson = blocksJson;
        set.SemanticsJson = semanticsJson;
    }

    private static LanguageFileSet CreateNewSet(string languageKey, string blocksJson, string semanticsJson)
    {
        return new LanguageFileSet
        {
            Id = Guid.NewGuid(),
            LanguageKey = languageKey,
            BlocksJson = blocksJson,
            SemanticsJson = semanticsJson
        };
    }
}