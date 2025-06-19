using Microsoft.AspNetCore.Mvc;
using Reservou.Domain.Cadastro;
using Reservou.HttpApi.ViewModels;

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
            return BadRequest("Dados de cadastro inválidos.");
        }

        var result = await cadastroHandler.NovoCadastro(new CadastroCommand
        {
            Nome = cadastroViewModel.Nome,
            Cpf = cadastroViewModel.Cpf,
            Email = cadastroViewModel.Email,
            Telefone = cadastroViewModel.Telefone,
            Senha = cadastroViewModel.Senha
        });


        if (!result)
        {
            return BadRequest("Erro ao cadastrar usuário. Tente novamente!");
        }

        return Accepted();
    }
}
