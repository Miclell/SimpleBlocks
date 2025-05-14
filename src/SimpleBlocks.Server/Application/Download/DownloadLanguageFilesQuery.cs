using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Domain.Entities;
using SimpleBlocks.Shared.Dto;

namespace SimpleBlocks.Server.Application.Download;

public record GetLanguageFilesQuery(string RequestedLanguageKey) : IRequest<Result<LanguageFileSet>>;