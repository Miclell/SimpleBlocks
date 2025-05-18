using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlocks.Server.Application.Execution;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Dto.CodeExecution;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Server.Api.Execution;

[ApiController]
[Route("api/[controller]")]
public class ExecutionCodeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExecutionCodeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("execute")]
    [ProducesResponseType(typeof(ApiResponse<Response>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExecuteCode([FromBody] Request request)
    {
        var command = new ExecuteCodeCommand(
            request.Code,
            request.LanguageId,
            request.Stdin ?? "");

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return Ok(ApiResponse<Response>.Success(result.Value!));
        }

        return BadRequest(ApiResponse<object>.Error(result.Error));
    }
}