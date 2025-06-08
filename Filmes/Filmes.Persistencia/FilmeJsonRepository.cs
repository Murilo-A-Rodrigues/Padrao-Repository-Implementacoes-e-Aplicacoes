using Filmes.Model;

namespace Filmes.Persistencia;

public class FilmeJsonRepository : GenericJsonRepository<Filme>, IFilmeRepository
{
    public FilmeJsonRepository() : base("filmes.json") { }

    public IEnumerable<Filme> ObterPorGenero(string genero)
    {
        return ObterTodos().Where(f =>
        f.Genero
         .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
         .Any(g => g.Equals(genero, StringComparison.OrdinalIgnoreCase)));
    }
}