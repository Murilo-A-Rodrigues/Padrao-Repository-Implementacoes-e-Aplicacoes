using CursosOnline.Model;

namespace CursosOnline.Persistencia;

public class CursoOnlineJsonRepository : GenericJsonRepository<CursoOnline>, ICursoOnlineRepository
{
    public CursoOnlineJsonRepository() : base("cursos.json") { }

    public CursoOnline? ObterPorNome(string nome)
    {
        return ObterTodos().FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
    }
}