using EquipamentosTI.Model;
using EquipamentosTI.Persistencia;

class Program
{
    static void Main()
    {
        IEquipamentoTIRepository repo = new InMemoryEquipamentoTIRepository();

        while (true)
        {
            Console.WriteLine("\n--- MENU EQUIPAMENTOS DE TI ---");
            Console.WriteLine("1 - Adicionar equipamento");
            Console.WriteLine("2 - Listar equipamentos");
            Console.WriteLine("3 - Buscar equipamento por ID");
            Console.WriteLine("4 - Remover equipamento");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var eq = new EquipamentoTI();
                eq.Id = Guid.NewGuid();
                Console.Write("Nome do equipamento: ");
                eq.NomeEquipamento = Console.ReadLine() ?? "";
                Console.Write("Tipo do equipamento (ex: Notebook, Monitor): ");
                eq.TipoEquipamento = Console.ReadLine() ?? "";
                Console.Write("Número de série: ");
                eq.NumeroSerie = Console.ReadLine() ?? "";

                // Validação da data de aquisição
                DateTime dataAquisicao;
                while (true)
                {
                    Console.Write("Data de aquisição (dd/MM/yyyy): ");
                    var entrada = Console.ReadLine();
                    if (!DateTime.TryParse(entrada, out dataAquisicao))
                    {
                        Console.WriteLine("Data inválida.");
                        continue;
                    }
                    if (dataAquisicao > DateTime.Today)
                    {
                        Console.WriteLine("A data de aquisição não pode ser no futuro.");
                        continue;
                    }
                    if (dataAquisicao.Year < 1980)
                    {
                        Console.WriteLine("Ano de aquisição muito antigo. Tente novamente.");
                        continue;
                    }
                    break;
                }
                eq.DataAquisicao = dataAquisicao;

                repo.Adicionar(eq);
                Console.WriteLine("Equipamento adicionado!");
            }
            else if (opcao == "2")
            {
                var equipamentos = repo.ObterTodos().ToList();
                if (equipamentos.Count == 0)
                {
                    Console.WriteLine("Nenhum equipamento cadastrado.");
                }
                else
                {
                    Console.WriteLine("--- Equipamentos Cadastrados ---");
                    foreach (var eq in equipamentos)
                    {
                        Console.WriteLine($"{eq.NomeEquipamento} | {eq.TipoEquipamento} | Série: {eq.NumeroSerie} | Aquisição: {eq.DataAquisicao:dd/MM/yyyy} | ID: {eq.Id}");
                    }
                }
            }
            else if (opcao == "3")
            {
                Console.Write("Digite o ID do equipamento para buscar: ");
                var idStr = Console.ReadLine();
                if (Guid.TryParse(idStr, out var idBusca))
                {
                    var encontrado = repo.ObterPorId(idBusca);
                    if (encontrado != null)
                        Console.WriteLine($"Encontrado: {encontrado.NomeEquipamento} ({encontrado.TipoEquipamento}) | Série: {encontrado.NumeroSerie} | Aquisição: {encontrado.DataAquisicao:dd/MM/yyyy}");
                    else
                        Console.WriteLine("Equipamento não encontrado.");
                }
                else
                {
                    Console.WriteLine("ID inválido.");
                }
            }
            else if (opcao == "4")
            {
                var equipamentos = repo.ObterTodos().ToList();
                if (equipamentos.Count == 0)
                {
                    Console.WriteLine("Nenhum equipamento para remover.");
                    continue;
                }
                for (int i = 0; i < equipamentos.Count; i++)
                {
                    var eq = equipamentos[i];
                    Console.WriteLine($"{i + 1} - {eq.NomeEquipamento} | {eq.TipoEquipamento} | Série: {eq.NumeroSerie}");
                }
                Console.Write("Digite o número do equipamento para remover: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= equipamentos.Count)
                {
                    var eq = equipamentos[idx - 1];
                    if (repo.Remover(eq.Id))
                        Console.WriteLine("Equipamento removido!");
                    else
                        Console.WriteLine("Erro ao remover equipamento.");
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