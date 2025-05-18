using FluentValidation;
using SimpleBlocks.Server.Domain.Constants;

namespace SimpleBlocks.Server.Application.Download;

public class GetLanguageFilesValidator : AbstractValidator<GetLanguageFilesQuery>
{
    public GetLanguageFilesValidator()
    {
        RuleFor(x => x.RequestedLanguageKey)
            .NotEmpty()
            .WithMessage("Ключ языка не может быть пустым")
            .MaximumLength(ValidationConstants.MaxLanguageKeyLength)
            .WithMessage($"Ключ языка не должен превышать {ValidationConstants.MaxLanguageKeyLength} символов");
    }
} 