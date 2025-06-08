using System.Text.Json;
using Hotel.Model;

namespace Hotel.Persistencia;

public class GenericJsonRepository<T> where T : IEntidade
{
    private readonly string _arquivo;
    private List<T> _itens;

    public GenericJsonRepository(string arquivo = "")
    {
        _arquivo = string.IsNullOrWhiteSpace(arquivo) ? typeof(T).Name.ToLower() + "s.json" : arquivo;
        _itens = Carregar();
    }

    public void Adicionar(T entidade)
    {
        _itens.Add(entidade);
        Salvar();
    }

    public IEnumerable<T> ObterTodos() => new List<T>(_itens);

    public T? ObterPorId(Guid id)
    {
        return _itens.FirstOrDefault(e => e.Id == id);
    }

    public void Atualizar(T entidade)
    {
        var idx = _itens.FindIndex(e => e.Id == entidade.Id);
        if (idx >= 0)
        {
            _itens[idx] = entidade;
            Salvar();
        }
    }

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