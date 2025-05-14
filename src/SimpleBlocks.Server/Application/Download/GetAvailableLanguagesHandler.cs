using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Domain.Interfaces;

namespace SimpleBlocks.Server.Application.Download;

public class GetAvailableLanguagesHandler : IRequestHandler<GetAvailableLanguagesQuery, Result<IEnumerable<string>>>
{
    private readonly ILanguageFileSetRepository _repository;

    public GetAvailableLanguagesHandler(ILanguageFileSetRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<string>>> Handle(GetAvailableLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languageKeys = await _repository.GetAllLanguageKeysAsync(cancellationToken);
        return Result<IEnumerable<string>>.Success(languageKeys);
    }
} 