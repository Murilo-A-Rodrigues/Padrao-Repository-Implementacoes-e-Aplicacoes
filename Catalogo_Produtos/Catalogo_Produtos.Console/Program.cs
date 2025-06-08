using Catalogo_Produtos.Model;
using Catalogo_Produtos.Persistencia;

class Program
{
    static void Main()
    {
        IProdutoRepository repo = new ProdutoJsonRepository();
        while (true)
        {
            Console.WriteLine("\n--- MENU PRODUTOS ---");
            Console.WriteLine("1 - Adicionar produto");
            Console.WriteLine("2 - Listar produtos");
            Console.WriteLine("3 - Remover produto");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var produto = new Produto();
                produto.Id = Guid.NewGuid();
                Console.Write("Nome: ");
                produto.Nome = Console.ReadLine() ?? "";
                Console.Write("Descrição: ");
                produto.Descricao = Console.ReadLine() ?? "";
                Console.Write("Preço: ");
                produto.Preco = decimal.TryParse(Console.ReadLine(), out var preco) ? preco : 0;
                Console.Write("Estoque: ");
                produto.Estoque = int.TryParse(Console.ReadLine(), out var estoque) ? estoque : 0;
                repo.Adicionar(produto);
                Console.WriteLine("Produto adicionado!");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- Lista de Produtos ---");
                var produtos = repo.ObterTodos();
                for (int i = 0; i < produtos.Count; i++)
                {
                    var p = produtos[i];
                    Console.WriteLine($"{i + 1} - {p.Nome} | {p.Descricao} | {p.Preco:C} | Estoque: {p.Estoque} | ID: {p.Id}");
                }
            }
            else if (opcao == "3")
            {
                var produtos = repo.ObterTodos();
                if (produtos.Count == 0)
                {
                    Console.WriteLine("Nenhum produto para remover.");
                    continue;
                }
                Console.WriteLine("\n--- Produtos para Remover ---");
                for (int i = 0; i < produtos.Count; i++)
                {
                    var p = produtos[i];
                    Console.WriteLine($"{i + 1} - {p.Nome} | {p.Descricao} | {p.Preco:C} | Estoque: {p.Estoque}");
                }
                Console.Write("Digite o número do produto para remover: ");
                if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= produtos.Count)
                {
                    var produto = produtos[indice - 1];
                    if (repo.Remover(produto.Id))
                        Console.WriteLine("Produto removido!");
                    else
                        Console.WriteLine("Erro ao remover produto.");
                }
                else
                {
                    Console.WriteLine("Número inválido.");
                }
            }
            else if (opcao == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }
}