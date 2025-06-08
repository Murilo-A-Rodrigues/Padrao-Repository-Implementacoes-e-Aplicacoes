using CursosOnline.Model;

namespace CursosOnline.Persistencia;

public interface ICursoOnlineRepository : IRepository<CursoOnline>
{
    CursoOnline? ObterPorNome(string nome);
}