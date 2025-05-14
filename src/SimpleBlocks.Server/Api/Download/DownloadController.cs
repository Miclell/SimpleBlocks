using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlocks.Server.Application.Common.Mapping;
using SimpleBlocks.Server.Application.Download;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Server.Api.Download;

[ApiController]
[Route("api/[controller]")]
public class DownloadController : ControllerBase
{
    private readonly IMediator _mediator;

    public DownloadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{requestedLanguageKey}")]
    [ProducesResponseType(typeof(ApiResponse<LanguageFilesDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string requestedLanguageKey, CancellationToken cancellationToken)
    {
        var query = new GetLanguageFilesQuery(requestedLanguageKey);
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            var dto = LanguageFilesMapper.ToDto(result.Value!);
            return Ok(ApiResponse<LanguageFilesDto>.Success(dto));
        }

        return NotFound(ApiResponse<object>.Error(result.Error));
    }
}