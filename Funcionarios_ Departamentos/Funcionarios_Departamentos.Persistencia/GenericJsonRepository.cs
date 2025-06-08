using System.Text.Json;
using Funcionarios.Model;

namespace Funcionarios.Persistencia;

public class GenericJsonRepository<T> where T : IEntidade
{
    private readonly string _arquivo;
    private List<T> _itens;

    public GenericJsonRepository()
    {
        _arquivo = typeof(T).Name.ToLower() + "s.json";
        _itens = Carregar();
    }

    public void Adicionar(T entidade)
    {
        _itens.Add(entidade);
        Salvar();
    }

    public List<T> ObterTodos() => new List<T>(_itens);

    public T? ObterPorId(Guid id) => _itens.FirstOrDefault(e => e.Id == id);

    public bool Remover(Guid id)
    {
        var entidade = _itens.FirstOrDefault(e => e.Id == id);
        if (entidade != null)
        {
            _itens.Remove(entidade);
            Salvar();
            return true;
        }
        return false;
    }

    private List<T> Carregar()
    {
        if (!File.Exists(_arquivo))
            return new List<T>();
        var json = File.ReadAllText(_arquivo);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    private void Salvar()
    {
        var json = JsonSerializer.Serialize(_itens, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_arquivo, json);
    }
}