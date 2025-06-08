using Pacientes.Model;

namespace Pacientes.Persistencia;

public interface IPacienteRepository : IRepository<Paciente>
{
    IEnumerable<Paciente> ObterPorFaixaEtaria(int idadeMinima, int idadeMaxima);
}