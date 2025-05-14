using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Dto.CodeExecution;

namespace SimpleBlocks.Server.Application.Execution;

public record ExecuteCodeCommand(
    string Code,
    int LanguageId,
    string Stdin) : IRequest<Result<Response>>; 