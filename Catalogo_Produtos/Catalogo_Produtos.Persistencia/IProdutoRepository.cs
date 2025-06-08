using Catalogo_Produtos.Model;

namespace Catalogo_Produtos.Persistencia;

public interface IProdutoRepository
{
    void Adicionar(Produto produto);
    Produto? ObterPorId(Guid id);
    List<Produto> ObterTodos();
    void Atualizar(Produto produto);
    bool Remover(Guid id);
}