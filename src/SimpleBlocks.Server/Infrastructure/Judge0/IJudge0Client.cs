using SimpleBlocks.Server.Infrastructure.Judge0.Models;

namespace SimpleBlocks.Server.Infrastructure.Judge0;

public interface IJudge0Client
{
    Task<SubmissionResponse> SubmitCodeAsync(SubmissionRequest request, CancellationToken cancellationToken = default);
    Task<SubmissionResponse> GetSubmissionAsync(string token, CancellationToken cancellationToken = default);
}