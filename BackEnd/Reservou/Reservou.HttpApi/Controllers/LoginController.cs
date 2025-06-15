using Microsoft.AspNetCore.Mvc;
using Reservou.HttpApi.ViewModels;

namespace Reservou.HttpApi.Controllers;

[Route("api/v{ApiVersion}/[Controller]")]
[ApiVersion("1")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UserLogin(
        [FromBody] LoginViewModel loginViewModel
    )
    {
        if (string.IsNullOrWhiteSpace(loginViewModel.Username) &&
            string.IsNullOrWhiteSpace(loginViewModel.Password))
        {
            Console.WriteLine("As informações do usuário não podem ser nulas ou estar em branco");
            return BadRequest();
        }

        // TODO validar aqui a com o banco de dados.

        return Accepted();
    }
}
