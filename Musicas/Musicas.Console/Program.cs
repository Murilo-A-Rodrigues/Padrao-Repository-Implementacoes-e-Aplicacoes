using Musicas.Model;
using Musicas.Persistencia;

class Program
{
    static void Main()
    {
        var repo = new GenericJsonRepository<Musica>();

        while (true)
        {
            Console.WriteLine("\n--- MENU MÚSICAS ---");
            Console.WriteLine("1 - Adicionar música");
            Console.WriteLine("2 - Listar músicas");
            Console.WriteLine("3 - Buscar música por ID");
            Console.WriteLine("4 - Remover música");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var musica = new Musica();
                musica.Id = Guid.NewGuid();
                Console.Write("Título: ");
                musica.Titulo = Console.ReadLine() ?? "";
                Console.Write("Artista: ");
                musica.Artista = Console.ReadLine() ?? "";
                Console.Write("Álbum: ");
                musica.Album = Console.ReadLine() ?? "";
                Console.Write("Duração (mm:ss): ");
                if (TimeSpan.TryParseExact(Console.ReadLine(), @"m\:ss", null, out var duracao))
                    musica.Duracao = duracao;
                else
                    musica.Duracao = TimeSpan.Zero;
                repo.Adicionar(musica);
                Console.WriteLine("Música adicionada!");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- Lista de Músicas ---");
                var musicas = repo.ObterTodos();
                for (int i = 0; i < musicas.Count; i++)
                {
                    var m = musicas[i];
                    Console.WriteLine($"{i + 1} - {m.Titulo} | {m.Artista} | {m.Album} | {m.Duracao:mm\\:ss} | ID: {m.Id}");
                }
            }
            else if (opcao == "3")
            {
                Console.Write("Digite o ID da música: ");
                var idStr = Console.ReadLine();
                if (Guid.TryParse(idStr, out var id))
                {
                    var musica = repo.ObterPorId(id);
                    if (musica != null)
                        Console.WriteLine($"{musica.Titulo} | {musica.Artista} | {musica.Album} | {musica.Duracao:mm\\:ss}");
                    else
                        Console.WriteLine("Música não encontrada.");
                }
                else
                {
                    Console.WriteLine("ID inválido.");
                }
            }
            else if (opcao == "4")
            {
                var musicas = repo.ObterTodos();
                if (musicas.Count == 0)
                {
                    Console.WriteLine("Nenhuma música para remover.");
                    continue;
                }
                for (int i = 0; i < musicas.Count; i++)
                {
                    var m = musicas[i];
                    Console.WriteLine($"{i + 1} - {m.Titulo} | {m.Artista} | {m.Album} | {m.Duracao:mm\\:ss}");
                }
                Console.Write("Digite o número da música para remover: ");
                if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= musicas.Count)
                {
                    var musica = musicas[indice - 1];
                    if (repo.Remover(musica.Id))
                        Console.WriteLine("Música removida!");
                    else
                        Console.WriteLine("Erro ao remover música.");
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