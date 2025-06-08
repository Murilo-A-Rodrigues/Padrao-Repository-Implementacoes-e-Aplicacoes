using System.Text.Json;
using Musicas.Model;

namespace Musicas.Persistencia;

public class GenericJsonRepository<T> where T : IEntidade
{
    private readonly string _arquivo;
    private List<T> _itens;

    public GenericJsonRepository()
    {
        var nome = typeof(T).Name.ToLower() + "s.json";
        _arquivo = nome;
        _itens = Carregar();
    }

    public void Adicionar(T item)
    {
        _itens.Add(item);
        Salvar();
    }

    public List<T> ObterTodos() => new List<T>(_itens);

    public T? ObterPorId(Guid id) => _itens.FirstOrDefault(x => x.Id == id);

    public bool Remover(Guid id)
    {
        var item = _itens.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            _itens.Remove(item);
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