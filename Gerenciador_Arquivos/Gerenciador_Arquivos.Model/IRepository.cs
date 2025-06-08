namespace GerenciadorArquivos.Model;

public interface IRepository<T> where T : IEntidade
{
    void Adicionar(T entidade);
    IEnumerable<T> ObterTodos();
    T? ObterPorId(Guid id);
    void Atualizar(T entidade);
    bool Remover(Guid id);
}