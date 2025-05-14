using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Domain.Entities;
using SimpleBlocks.Server.Domain.Interfaces;

namespace SimpleBlocks.Server.Application.Download;

public class GetLanguageFilesHandler : IRequestHandler<GetLanguageFilesQuery, Result<LanguageFileSet>>
{
    private readonly ILanguageFileSetRepository _repository;

    public GetLanguageFilesHandler(ILanguageFileSetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<LanguageFileSet>> Handle(GetLanguageFilesQuery request, CancellationToken cancellationToken)
    {
        var languageFiles = await _repository.GetByLanguageKeyAsync(request.RequestedLanguageKey, cancellationToken);

        if (languageFiles == null)
            return Result<LanguageFileSet>.CreateError($"Файлы для языка '{request.RequestedLanguageKey}' не найдены.");

        return Result<LanguageFileSet>.Success(languageFiles);
    }
}