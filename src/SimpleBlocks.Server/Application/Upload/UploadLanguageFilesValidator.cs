using FluentValidation;
using SimpleBlocks.Server.Domain.Constants;
using System.Text.Json;

namespace SimpleBlocks.Server.Application.Upload;

public class UploadLanguageFilesValidator : AbstractValidator<UploadLanguageFilesCommand>
{
    public UploadLanguageFilesValidator()
    {
        RuleFor(x => x.BlocksJson)
            .NotEmpty()
            .WithMessage("JSON блоков не может быть пустым")
            .Must(BeValidJson)
            .WithMessage("JSON блоков имеет неверный формат")
            .MaximumLength(ValidationConstants.MaxJsonLength)
            .WithMessage($"Размер JSON блоков не должен превышать {ValidationConstants.MaxJsonLength / 1024 / 1024}MB");

        RuleFor(x => x.SemanticsJson)
            .NotEmpty()
            .WithMessage("JSON семантики не может быть пустым")
            .Must(BeValidJson)
            .WithMessage("JSON семантики имеет неверный формат")
            .MaximumLength(ValidationConstants.MaxJsonLength)
            .WithMessage($"Размер JSON семантики не должен превышать {ValidationConstants.MaxJsonLength / 1024 / 1024}MB");

        RuleFor(x => x.LanguageKey)
            .NotEmpty()
            .WithMessage("Ключ языка не может быть пустым")
            .MaximumLength(ValidationConstants.MaxLanguageKeyLength)
            .WithMessage($"Ключ языка не должен превышать {ValidationConstants.MaxLanguageKeyLength} символов");
    }

    private static bool BeValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }
} 