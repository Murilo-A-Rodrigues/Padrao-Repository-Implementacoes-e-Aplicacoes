using CursosOnline.Model;
using CursosOnline.Persistencia;
using CursosOnline.Servico;

class Program
{
    static void Main()
    {
        var repo = new CursoOnlineJsonRepository();
        var servico = new CatalogoCursosService(repo);

        while (true)
        {
            Console.WriteLine("\n--- MENU CURSOS ONLINE ---");
            Console.WriteLine("1 - Cadastrar novo curso");
            Console.WriteLine("2 - Listar cursos cadastrados");
            Console.WriteLine("3 - Excluir curso");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                Console.Write("Nome do curso: ");
                var nome = Console.ReadLine() ?? "";
                Console.Write("Instrutor: ");
                var instrutor = Console.ReadLine() ?? "";
                Console.Write("Carga horária: ");
                int.TryParse(Console.ReadLine(), out var carga);

                var curso = new CursoOnline
                {
                    Id = Guid.NewGuid(),
                    Nome = nome,
                    Instrutor = instrutor,
                    CargaHoraria = carga
                };

                if (servico.RegistrarCurso(curso))
                    Console.WriteLine("Curso cadastrado!");
                else
                    Console.WriteLine("Já existe um curso com esse nome.");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- Cursos cadastrados ---");
                var cursos = servico.ListarCursos().ToList();
                if (cursos.Count == 0)
                    Console.WriteLine("Nenhum curso cadastrado.");
                else
                    foreach (var c in cursos)
                        Console.WriteLine($"{c.Nome} | Instrutor: {c.Instrutor} | Carga: {c.CargaHoraria}h");
            }
            else if (opcao == "3")
            {
                var cursos = servico.ListarCursos().ToList();
                if (cursos.Count == 0)
                {
                    Console.WriteLine("Nenhum curso para excluir.");
                    continue;
                }
                Console.WriteLine("\n--- Cursos cadastrados ---");
                for (int i = 0; i < cursos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {cursos[i].Nome} | Instrutor: {cursos[i].Instrutor} | Carga: {cursos[i].CargaHoraria}h");
                }
                Console.Write("Digite o número do curso para excluir: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= cursos.Count)
                {
                    var curso = cursos[idx - 1];
                    if (repo.Remover(curso.Id))
                        Console.WriteLine("Curso removido!");
                    else
                        Console.WriteLine("Erro ao remover curso.");
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