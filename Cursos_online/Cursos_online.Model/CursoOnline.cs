namespace CursosOnline.Model;

public class CursoOnline : IEntidade
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Instrutor { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
}