using GerenciadorArquivos.Model;

namespace GerenciadorArquivos.Persistencia;

public interface IArquivoDigitalRepository : IRepository<ArquivoDigital>
{
    IEnumerable<ArquivoDigital> BuscarPorTipo(string tipoArquivo);
}