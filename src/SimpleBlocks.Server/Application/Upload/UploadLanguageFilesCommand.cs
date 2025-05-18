using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Domain.Entities;

namespace SimpleBlocks.Server.Application.Upload;

// Application/Upload/UploadFileCommand.cs
public record UploadLanguageFilesCommand(
    string LanguageKey,
    string BlocksJson,
    string SemanticsJson) : IRequest<Result<LanguageFileSet>>;
