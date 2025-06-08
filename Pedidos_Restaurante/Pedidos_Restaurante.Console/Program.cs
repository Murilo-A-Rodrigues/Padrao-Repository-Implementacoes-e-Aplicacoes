using Pedidos_Restaurante.Model;
using Pedidos_Restaurante.Persistencia;

class Program
{
    static void Main()
    {
        var repo = new GenericJsonRepository<ItemCardapio>("cardapio.json");

        while (true)
        {
            Console.WriteLine("\n--- MENU CARDÁPIO ---");
            Console.WriteLine("1 - Adicionar Prato");
            Console.WriteLine("2 - Adicionar Bebida");
            Console.WriteLine("3 - Listar todos os itens do cardápio");
            Console.WriteLine("4 - Excluir item do cardápio");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var prato = new Prato();
                prato.Id = Guid.NewGuid();
                Console.Write("Nome do prato: ");
                prato.NomeItem = Console.ReadLine() ?? "";
                Console.Write("Preço: ");
                decimal.TryParse(Console.ReadLine(), out var preco);
                prato.Preco = preco;
                Console.Write("Descrição detalhada: ");
                prato.DescricaoDetalhada = Console.ReadLine() ?? "";
                Console.Write("É vegetariano? (s/n): ");
                prato.Vegetariano = (Console.ReadLine() ?? "").Trim().ToLower() == "s";
                repo.Adicionar(prato);
                Console.WriteLine("Prato adicionado!");
            }
            else if (opcao == "2")
            {
                var bebida = new Bebida();
                bebida.Id = Guid.NewGuid();
                Console.Write("Nome da bebida: ");
                bebida.NomeItem = Console.ReadLine() ?? "";
                Console.Write("Preço: ");
                decimal.TryParse(Console.ReadLine(), out var preco);
                bebida.Preco = preco;
                Console.Write("Volume (ml): ");
                int.TryParse(Console.ReadLine(), out var volume);
                bebida.VolumeMl = volume;
                Console.Write("É alcoólica? (s/n): ");
                bebida.Alcoolica = (Console.ReadLine() ?? "").Trim().ToLower() == "s";
                repo.Adicionar(bebida);
                Console.WriteLine("Bebida adicionada!");
            }
            else if (opcao == "3")
            {
                Console.WriteLine("\n--- Itens do Cardápio ---");
                var itens = repo.ObterTodos().ToList();
                if (itens.Count == 0)
                {
                    Console.WriteLine("Nenhum item cadastrado.");
                }
                else
                {
                    foreach (var item in itens)
                    {
                        if (item is Prato prato)
                            Console.WriteLine($"Prato: {prato.NomeItem} | {prato.Preco:C} | {prato.DescricaoDetalhada} | Vegetariano: {(prato.Vegetariano ? "Sim" : "Não")}");
                        else if (item is Bebida bebida)
                            Console.WriteLine($"Bebida: {bebida.NomeItem} | {bebida.Preco:C} | {bebida.VolumeMl}ml | Alcoólica: {(bebida.Alcoolica ? "Sim" : "Não")}");
                    }
                }
            }
            else if (opcao == "4")
            {
                var itens = repo.ObterTodos().ToList();
                if (itens.Count == 0)
                {
                    Console.WriteLine("Nenhum item para remover.");
                    continue;
                }
                Console.WriteLine("--- Itens do Cardápio ---");
                for (int i = 0; i < itens.Count; i++)
                {
                    var item = itens[i];
                    if (item is Prato prato)
                        Console.WriteLine($"{i + 1} - Prato: {prato.NomeItem} | {prato.Preco:C}");
                    else if (item is Bebida bebida)
                        Console.WriteLine($"{i + 1} - Bebida: {bebida.NomeItem} | {bebida.Preco:C}");
                }
                Console.Write("Digite o número do item para remover: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= itens.Count)
                {
                    var item = itens[idx - 1];
                    if (repo.Remover(item.Id))
                        Console.WriteLine("Item removido!");
                    else
                        Console.WriteLine("Erro ao remover item.");
                }
                else
                {
                    Console.WriteLine("Número inválido.");
                }
            }
            else if (opcao == "5")
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