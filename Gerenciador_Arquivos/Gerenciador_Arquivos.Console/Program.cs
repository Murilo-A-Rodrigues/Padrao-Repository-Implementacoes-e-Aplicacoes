using GerenciadorArquivos.Model;
using GerenciadorArquivos.Persistencia;

class Program
{
    static void Main()
    {
        IArquivoDigitalRepository repo = new ArquivoDigitalJsonRepository();

        while (true)
        {
            Console.WriteLine("\n--- GERENCIADOR DE ARQUIVOS DIGITAIS ---");
            Console.WriteLine("1 - Adicionar arquivo digital");
            Console.WriteLine("2 - Listar todos os arquivos");
            Console.WriteLine("3 - Buscar arquivos por tipo");
            Console.WriteLine("4 - Remover arquivo");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var arquivo = new ArquivoDigital();
                arquivo.Id = Guid.NewGuid();
                Console.Write("Nome do arquivo: ");
                arquivo.NomeArquivo = Console.ReadLine() ?? "";
                Console.Write("Tipo do arquivo (ex: pdf, jpg): ");
                arquivo.TipoArquivo = Console.ReadLine() ?? "";
                Console.Write("Tamanho em bytes: ");
                long.TryParse(Console.ReadLine(), out var tamanho);
                arquivo.TamanhoBytes = tamanho;
                arquivo.DataUpload = DateTime.Now;
                repo.Adicionar(arquivo);
                Console.WriteLine("Arquivo adicionado!");
            }
            else if (opcao == "2")
            {
                var arquivos = repo.ObterTodos().ToList();
                if (arquivos.Count == 0)
                {
                    Console.WriteLine("Nenhum arquivo cadastrado.");
                }
                else
                {
                    Console.WriteLine("--- Arquivos Digitais ---");
                    foreach (var arq in arquivos)
                    {
                        Console.WriteLine($"{arq.NomeArquivo} | {arq.TipoArquivo} | {arq.TamanhoBytes} bytes | {arq.DataUpload:dd/MM/yyyy HH:mm} | ID: {arq.Id}");
                    }
                }
            }
            else if (opcao == "3")
            {
                Console.Write("Digite o tipo de arquivo para buscar (ex: pdf): ");
                var tipo = Console.ReadLine() ?? "";
                var arquivos = repo.BuscarPorTipo(tipo).ToList();
                if (arquivos.Count == 0)
                {
                    Console.WriteLine("Nenhum arquivo encontrado para esse tipo.");
                }
                else
                {
                    Console.WriteLine("--- Arquivos Encontrados ---");
                    foreach (var arq in arquivos)
                    {
                        Console.WriteLine($"{arq.NomeArquivo} | {arq.TamanhoBytes} bytes | {arq.DataUpload:dd/MM/yyyy HH:mm}");
                    }
                }
            }
            else if (opcao == "4")
            {
                var arquivos = repo.ObterTodos().ToList();
                if (arquivos.Count == 0)
                {
                    Console.WriteLine("Nenhum arquivo para remover.");
                    continue;
                }
                for (int i = 0; i < arquivos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {arquivos[i].NomeArquivo} | {arquivos[i].TipoArquivo} | {arquivos[i].TamanhoBytes} bytes");
                }
                Console.Write("Digite o número do arquivo para remover: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= arquivos.Count)
                {
                    var arq = arquivos[idx - 1];
                    if (repo.Remover(arq.Id))
                        Console.WriteLine("Arquivo removido!");
                    else
                        Console.WriteLine("Erro ao remover arquivo.");
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