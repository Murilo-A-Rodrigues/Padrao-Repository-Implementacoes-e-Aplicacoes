namespace Funcionarios.Model;

public class Funcionario : IEntidade
{
    public Guid Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public Guid DepartamentoId { get; set; }
}