using Reservou.Domain.Cadastro.Infrastructure;
using System.Net;

namespace Reservou.Domain.Cadastro;

public class CadastroHandler
{
    private readonly CadastroRepository _cadastroRepository;

    public CadastroHandler(CadastroRepository cadastroRepository) 
    {
        _cadastroRepository = cadastroRepository;
    }

    public async Task<HttpStatusCode> NovoCadastro(CadastroCommand cadastro)
    {
        if (cadastro == null)
        {
            throw new ArgumentNullException(nameof(cadastro), "Cadastro não pode ser nulo.");
        }

        return await _cadastroRepository.AddCadastro(cadastro);
    }
}
