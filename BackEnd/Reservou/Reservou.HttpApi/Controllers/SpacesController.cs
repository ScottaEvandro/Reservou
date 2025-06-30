using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Spaces.Commands;
using Reservou.Domain.Spaces.Dtos;
using Reservou.Domain.Spaces.Handlers;
using Reservou.Domain.Spaces.Infrastructure;
using Reservou.HttpApi.ViewModels.Spaces;
using System.Text.Json;

namespace Reservou.HttpApi.Controllers;

[Route("api/v{ApiVersion}/[Controller]")]
[ApiVersion("1")]
public class SpacesController
{
    [HttpGet("GetSpacesTypes")]
    public async Task<IActionResult> GetSpaces(
        [FromServices] SpacesQueries spacesQueries
    )
    {
        var result = await spacesQueries.GetSpaces();

        return new ObjectResult(result);
    }

    [HttpPost("AddSpace")]
    public async Task<IActionResult> AddSpace(
        [FromForm] NewSpaceViewModel newSpace,
        List<IFormFile> SpaceImages,
        [FromServices] SpaceHandler spaceHandler
    )
    {
        NewSpaceCommand newSpaceCommand = new(
            newSpace.SpaceName,
            newSpace.Description,
            newSpace.Capacity,
            newSpace.SpaceTypeId,
            newSpace.Price,
            newSpace.ReserveDuration,
            newSpace.MaintenanceTime,
            newSpace.isActive
        );

        var result = await spaceHandler.CreateNewSpaceAsync(newSpaceCommand, SpaceImages);

        return result switch
        {
            true => new StatusCodeResult(StatusCodes.Status201Created),
            false => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpGet("GetAllSpaces")]
    public async Task<IActionResult> GetAllSpaces(
        [FromServices] SpacesQueries spacesQueries)
    {
        var result = await spacesQueries.GetAllSpaces();

        return new ObjectResult(result);
    }
}
