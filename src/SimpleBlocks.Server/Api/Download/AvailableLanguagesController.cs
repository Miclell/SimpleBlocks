using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlocks.Server.Application.Download;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Server.Api.Download;

[ApiController]
[Route("api/[controller]")]
public class AvailableLanguagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AvailableLanguagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetAvailableLanguagesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<IEnumerable<string>>.Success(result.Value!));
        }

        return BadRequest(ApiResponse<object>.Error(result.Error));
    }
} 