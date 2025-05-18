using MediatR;
using SimpleBlocks.Server.Application.Common;

namespace SimpleBlocks.Server.Application.Download;

public record GetAvailableLanguagesQuery : IRequest<Result<IEnumerable<string>>>; 