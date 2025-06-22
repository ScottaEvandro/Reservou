using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Login;
using Reservou.HttpApi.ViewModels;

namespace Reservou.HttpApi.Controllers;

[Route("api/v{ApiVersion}/[Controller]")]
[ApiVersion("1")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UserLogin(
        [FromBody] LoginViewModel loginViewModel,
        [FromServices] LoginHandler loginHandler
    )
    {
        if (string.IsNullOrWhiteSpace(loginViewModel.Username) &&
            string.IsNullOrWhiteSpace(loginViewModel.Password))
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var result = await loginHandler.LoginStatus(
            loginViewModel.Username,
            loginViewModel.Password
        );

        if (!result)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }

        return StatusCode(StatusCodes.Status200OK);
    }
}
