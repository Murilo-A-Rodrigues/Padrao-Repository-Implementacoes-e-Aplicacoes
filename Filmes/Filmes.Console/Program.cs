using Filmes.Model;
using Filmes.Persistencia;

class Program
{
    static void Main()
    {
        IFilmeRepository repo = new FilmeJsonRepository();

        while (true)
        {
            Console.WriteLine("\n--- MENU FILMES ---");
            Console.WriteLine("1 - Adicionar filme");
            Console.WriteLine("2 - Listar todos os filmes");
            Console.WriteLine("3 - Buscar filmes por gênero");
            Console.WriteLine("4 - Remover filme");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var filme = new Filme();
                filme.Id = Guid.NewGuid();
                Console.Write("Título: ");
                filme.Titulo = Console.ReadLine() ?? "";
                Console.Write("Diretor: ");
                filme.Diretor = Console.ReadLine() ?? "";
                Console.Write("Ano de Lançamento: ");
                filme.AnoLancamento = int.TryParse(Console.ReadLine(), out var ano) ? ano : 0;
                Console.Write("Gênero (separe múltiplos por vírgula): ");
                filme.Genero = Console.ReadLine() ?? "";
                repo.Adicionar(filme);
                Console.WriteLine("Filme adicionado!");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- Lista de Filmes ---");
                var filmes = repo.ObterTodos().ToList();
                for (int i = 0; i < filmes.Count; i++)
                {
                    var f = filmes[i];
                    Console.WriteLine($"{i + 1} - {f.Titulo} | {f.Diretor} | {f.AnoLancamento} | {f.Genero} | ID: {f.Id}");
                }
            }
            else if (opcao == "3")
            {
                Console.Write("Digite o gênero para buscar: ");
                var genero = Console.ReadLine() ?? "";
                var filmes = repo.ObterPorGenero(genero).ToList();
                if (filmes.Count == 0)
                {
                    Console.WriteLine("Nenhum filme encontrado para esse gênero.");
                }
                else
                {
                    Console.WriteLine("\n--- Filmes Encontrados ---");
                    foreach (var f in filmes)
                        Console.WriteLine($"{f.Titulo} | {f.Diretor} | {f.AnoLancamento} | {f.Genero} | ID: {f.Id}");
                }
            }
            else if (opcao == "4")
            {
                var filmes = repo.ObterTodos().ToList();
                if (filmes.Count == 0)
                {
                    Console.WriteLine("Nenhum filme para remover.");
                    continue;
                }
                for (int i = 0; i < filmes.Count; i++)
                {
                    var f = filmes[i];
                    Console.WriteLine($"{i + 1} - {f.Titulo} | {f.Diretor} | {f.AnoLancamento} | {f.Genero}");
                }
                Console.Write("Digite o número do filme para remover: ");
                if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= filmes.Count)
                {
                    var filme = filmes[indice - 1];
                    if (repo.Remover(filme.Id))
                        Console.WriteLine("Filme removido!");
                    else
                        Console.WriteLine("Erro ao remover filme.");
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