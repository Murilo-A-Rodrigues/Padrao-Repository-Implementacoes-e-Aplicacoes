using CursosOnline.Model;
using CursosOnline.Persistencia;

namespace CursosOnline.Servico;

public class CatalogoCursosService
{
    private readonly ICursoOnlineRepository _repo;

    public CatalogoCursosService(ICursoOnlineRepository repo)
    {
        _repo = repo;
    }

    public bool RegistrarCurso(CursoOnline curso)
    {
        // Regra de negócio: não permitir cursos duplicados pelo nome
        if (_repo.ObterPorNome(curso.Nome) != null)
            return false;

        _repo.Adicionar(curso);
        return true;
    }

    public IEnumerable<CursoOnline> ListarCursos()
    {
        return _repo.ObterTodos();
    }
}