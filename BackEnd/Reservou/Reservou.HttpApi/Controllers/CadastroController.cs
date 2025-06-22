using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Cadastro;
using Reservou.HttpApi.ViewModels;
using System.Net;

namespace Reservou.HttpApi.Controllers;

[Route("api/v{version:ApiVersion}/[Controller]")]
[ApiVersion("1")]
public class CadastroController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> NovoCadastro(
        [FromBody] CadastroViewModel cadastroViewModel,
        [FromServices] CadastroHandler cadastroHandler)
    {
        if (cadastroViewModel == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        var result = await cadastroHandler.NovoCadastro(new CadastroCommand
        {
            Nome = cadastroViewModel.Username,
            Cpf = cadastroViewModel.Cpf,
            Email = cadastroViewModel.Email,
            Telefone = cadastroViewModel.Phone,
            Senha = cadastroViewModel.Password
        });

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
