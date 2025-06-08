using System.Text.Json;
using Catalogo_Produtos.Model;

namespace Catalogo_Produtos.Persistencia;

public class ProdutoJsonRepository : IProdutoRepository
{
    private readonly string _caminhoArquivo = "produtos.json";
    private List<Produto> _produtos;

    public ProdutoJsonRepository()
    {
        _produtos = Carregar();
    }

    public void Adicionar(Produto produto)
    {
        _produtos.Add(produto);
        Salvar();
    }

    public Produto? ObterPorId(Guid id)
    {
        return _produtos.FirstOrDefault(p => p.Id == id);
    }

    public List<Produto> ObterTodos()
    {
        return new List<Produto>(_produtos);
    }

    public void Atualizar(Produto produto)
    {
        var index = _produtos.FindIndex(p => p.Id == produto.Id);
        if (index >= 0)
        {
            _produtos[index] = produto;
            Salvar();
        }
    }

    public bool Remover(Guid id)
    {
        var produto = _produtos.FirstOrDefault(p => p.Id == id);
        if (produto != null)
        {
            _produtos.Remove(produto);
            Salvar();
            return true;
        }
        return false;
    }

    private List<Produto> Carregar()
    {
        if (!File.Exists(_caminhoArquivo))
            return new List<Produto>();

        var json = File.ReadAllText(_caminhoArquivo);
        return JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
    }

    private void Salvar()
    {
        var json = JsonSerializer.Serialize(_produtos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_caminhoArquivo, json);
    }
}