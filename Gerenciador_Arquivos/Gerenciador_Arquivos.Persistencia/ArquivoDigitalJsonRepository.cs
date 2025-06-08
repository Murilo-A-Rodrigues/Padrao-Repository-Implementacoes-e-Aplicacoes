using GerenciadorArquivos.Model;

namespace GerenciadorArquivos.Persistencia;

public class ArquivoDigitalJsonRepository : GenericJsonRepository<ArquivoDigital>, IArquivoDigitalRepository
{
    public ArquivoDigitalJsonRepository() : base("arquivosdigitais.json") { }

    public IEnumerable<ArquivoDigital> BuscarPorTipo(string tipoArquivo)
    {
        return ObterTodos().Where(a => a.TipoArquivo.Equals(tipoArquivo, StringComparison.OrdinalIgnoreCase));
    }
}