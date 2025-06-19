namespace Reservou.Domain.Cadastro;

#nullable disable
public class CadastroCommand
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Senha { get; set; }
    public int TipoUsuario => 0;
}
