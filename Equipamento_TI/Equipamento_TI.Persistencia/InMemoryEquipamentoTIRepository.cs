using EquipamentosTI.Model;

namespace EquipamentosTI.Persistencia;

public class InMemoryEquipamentoTIRepository : IEquipamentoTIRepository
{
    private readonly List<EquipamentoTI> _itens = new();

    public void Adicionar(EquipamentoTI entidade)
    {
        _itens.Add(entidade);
    }

    public IEnumerable<EquipamentoTI> ObterTodos() => new List<EquipamentoTI>(_itens);

    public EquipamentoTI? ObterPorId(Guid id) => _itens.FirstOrDefault(e => e.Id == id);

    public void Atualizar(EquipamentoTI entidade)
    {
        var idx = _itens.FindIndex(e => e.Id == entidade.Id);
        if (idx >= 0)
            _itens[idx] = entidade;
    }

    public bool Remover(Guid id)
    {
        var entidade = _itens.FirstOrDefault(e => e.Id == id);
        if (entidade != null)
        {
            _itens.Remove(entidade);
            return true;
        }
        return false;
    }
}