using System.Text.Json;
using Pedidos_Restaurante.Model;

namespace Pedidos_Restaurante.Persistencia;

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

        var options = new JsonSerializerOptions();
        if (typeof(T) == typeof(ItemCardapio))
            options.Converters.Add(new ItemCardapioPolymorphicConverter());

        var json = File.ReadAllText(_arquivo);
        return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
    }

    private void Salvar()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        if (typeof(T) == typeof(ItemCardapio))
            options.Converters.Add(new ItemCardapioPolymorphicConverter());

        var json = JsonSerializer.Serialize(_itens, options);
        File.WriteAllText(_arquivo, json);
    }
}