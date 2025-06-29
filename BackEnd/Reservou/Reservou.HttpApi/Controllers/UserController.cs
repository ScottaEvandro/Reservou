using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Users.Commands;
using Reservou.Domain.Users.Handlers;
using Reservou.HttpApi.ViewModels.Users;
using System.Net;

namespace Reservou.HttpApi.Controllers;

[Route("api/v{ApiVersion}/[Controller]")]
[ApiVersion("1")]
public class UserController : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> UserLogin(
        [FromBody] UserLoginViewModel loginViewModel,
        [FromServices] UserHandler loginHandler
    )
    {
        if (string.IsNullOrWhiteSpace(loginViewModel.Username) &&
            string.IsNullOrWhiteSpace(loginViewModel.Password))
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        var result = await loginHandler.GetUserLogin(
            loginViewModel.Username,
            loginViewModel.Password
        );

        if (result == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }

        return new ObjectResult(result);
    }

    [HttpPost("Cadastro")]
    public async Task<IActionResult> UserRegister(
        [FromBody] UserRegisterViewModel registerViewModel,
        [FromServices] UserHandler userHandler
        )
    {
        if (string.IsNullOrWhiteSpace(registerViewModel.Username) ||
            string.IsNullOrWhiteSpace(registerViewModel.TaxId) ||
            string.IsNullOrWhiteSpace(registerViewModel.PhoneNumber) ||
            string.IsNullOrWhiteSpace(registerViewModel.Email) ||
            string.IsNullOrWhiteSpace(registerViewModel.Password))
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        UserRegisterCommand newUser = new UserRegisterCommand
        {
            Username = registerViewModel.Username,
            TaxId = registerViewModel.TaxId,
            PhoneNumber = registerViewModel.PhoneNumber,
            Email = registerViewModel.Email,
            Password = registerViewModel.Password
        };

        var result = await userHandler.RegisterNewUser(newUser);

        switch (result)
        {
            case HttpStatusCode.Conflict:
                return StatusCode(StatusCodes.Status409Conflict);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return StatusCode(StatusCodes.Status201Created);
    }
}
