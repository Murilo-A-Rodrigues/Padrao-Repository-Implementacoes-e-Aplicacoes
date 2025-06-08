using Pacientes.Model;

namespace Pacientes.Persistencia;

public class PacienteJsonRepository : GenericJsonRepository<Paciente>, IPacienteRepository
{
    public PacienteJsonRepository() : base("pacientes.json") { }

    public IEnumerable<Paciente> ObterPorFaixaEtaria(int idadeMinima, int idadeMaxima)
    {
        var hoje = DateTime.Today;
        return ObterTodos().Where(p =>
        {
            var idade = hoje.Year - p.DataNascimento.Year;
            if (p.DataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade >= idadeMinima && idade <= idadeMaxima;
        });
    }
}