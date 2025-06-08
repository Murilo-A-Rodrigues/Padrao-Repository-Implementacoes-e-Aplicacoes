namespace Pedidos_Restaurante.Model;

public class Prato : ItemCardapio
{
    public string DescricaoDetalhada { get; set; } = string.Empty;
    public bool Vegetariano { get; set; }
}