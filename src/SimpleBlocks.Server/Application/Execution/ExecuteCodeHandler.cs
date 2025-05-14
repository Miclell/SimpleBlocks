using System.Text;
using MediatR;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Infrastructure.Judge0;
using SimpleBlocks.Server.Infrastructure.Judge0.Models;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Dto.CodeExecution;

namespace SimpleBlocks.Server.Application.Execution;

public class ExecuteCodeHandler : IRequestHandler<ExecuteCodeCommand, Result<Response>>
{
    private readonly IJudge0Client _judge0Client;
    private const int MaxAttempts = 10;
    private static readonly TimeSpan Delay = TimeSpan.FromSeconds(1);

    public ExecuteCodeHandler(IJudge0Client judge0Client)
    {
        _judge0Client = judge0Client;
    }

    public async Task<Result<Response>> Handle(ExecuteCodeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var submissionRequest = new SubmissionRequest(
                SourceCode: request.Code,
                LanguageId: request.LanguageId,
                Stdin: request.Stdin);

            var submission = await _judge0Client.SubmitCodeAsync(submissionRequest, cancellationToken);

            await Task.Delay(Delay, cancellationToken);

            for (var attempt = 0; attempt < MaxAttempts; attempt++)
            {
                var result = await _judge0Client.GetSubmissionAsync(submission.Token, cancellationToken);
                
                if (result.Status.Id != 1 && result.Status.Id != 2)
                {
                    return Result<Response>.Success(new Response(
                        Output: result.Stdout ?? string.Empty,
                        Error: result.Stderr,
                        CompileOutput: result.CompileOutput,
                        Message: result.Message,
                        IsSuccess: result.Status.Id == 3));
                }

                await Task.Delay(Delay, cancellationToken);
            }

            return Result<Response>.CreateError("Execution timed out");
        }
        catch (Exception ex)
        {
            return Result<Response>.CreateError($"Error executing code: {ex.Message}");
        }
    }
} 