using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Reserves.Command;
using Reservou.Domain.Reserves.Infrastructure;
using Reservou.HttpApi.ViewModels.Reserves;

namespace Reservou.HttpApi.Controllers;

[Route("api/v{ApiVersion}/[Controller]")]
[ApiVersion("1")]
public class ReserveController
{
    [HttpGet("GetTodayReserves")]
    public async Task<IActionResult> GetTodayReserves(
        [FromServices] ReserveQueries reserveQueries
    )
    {
        var result = await reserveQueries.GetTodayReserves();

        if (result == null || !result.Any())
        {
            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        return new ObjectResult(result);
    }

    [HttpPost("AddReserve")]
    public async Task<IActionResult> AddReserve(
        [FromBody] ReserveViewModel reserveViewModel,
        [FromServices] ReserveQueries reserveQueries,
        [FromServices] ReserveRepositories reserveRepositories
    )
    {
        var reserveTime = await reserveQueries.GetReserveDuration(reserveViewModel.SpaceId);

        var reserve = new SaveReserveCommand
        {
            SpaceId = reserveViewModel.SpaceId,
            UserId = reserveViewModel.UserId,
            StartDate = reserveViewModel.StartDate,
            Hours = reserveViewModel.Hours
        };

        var result = await reserveRepositories.SaveReserve(reserve, reserveTime);

        if (!result)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new StatusCodeResult(StatusCodes.Status201Created);
    }
}
