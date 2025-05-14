using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBlocks.Server.Application.Common;
using SimpleBlocks.Server.Application.Common.Mapping;
using SimpleBlocks.Server.Application.Upload;
using SimpleBlocks.Shared.Dto;
using SimpleBlocks.Shared.Models;

namespace SimpleBlocks.Server.Api.Upload;

[ApiController]
[Route("api/[controller]")]
public class UploadLanguageFilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UploadLanguageFilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<LanguageFilesDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromBody] UploadLanguageFilesRequest request)
    {
        var command = new UploadLanguageFilesCommand(
            request.LanguageKey,
            request.BlocksJson,
            request.SemanticsJson);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            var dto = LanguageFilesMapper.ToDto(result.Value!);
            return Ok(ApiResponse<LanguageFilesDto>.Success(dto));
        }

        return BadRequest(ApiResponse<object>.Error(result.Error));
    }
}

public record UploadLanguageFilesRequest(string LanguageKey, string BlocksJson, string SemanticsJson);