using Filmes.Model;

namespace Filmes.Persistencia;

public interface IFilmeRepository : IRepository<Filme>
{
    IEnumerable<Filme> ObterPorGenero(string genero);
}