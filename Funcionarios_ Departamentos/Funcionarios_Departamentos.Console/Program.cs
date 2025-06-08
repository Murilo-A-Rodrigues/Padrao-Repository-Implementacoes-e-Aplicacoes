using Funcionarios.Model;
using Funcionarios.Persistencia;

class Program
{
    static void Main()
    {
        var repoDepto = new GenericJsonRepository<Departamento>();
        var repoFunc = new GenericJsonRepository<Funcionario>();

        while (true)
        {
            Console.WriteLine("\n--- MENU FUNCIONÁRIOS E DEPARTAMENTOS ---");
            Console.WriteLine("1 - Cadastrar departamento");
            Console.WriteLine("2 - Cadastrar funcionário");
            Console.WriteLine("3 - Listar funcionários e seus departamentos");
            Console.WriteLine("4 - Excluir funcionário");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var depto = new Departamento();
                depto.Id = Guid.NewGuid();
                Console.Write("Nome do departamento: ");
                depto.NomeDepartamento = Console.ReadLine() ?? "";
                Console.Write("Sigla: ");
                depto.Sigla = Console.ReadLine() ?? "";
                repoDepto.Adicionar(depto);
                Console.WriteLine("Departamento cadastrado!");
            }
            else if (opcao == "2")
            {
                var departamentos = repoDepto.ObterTodos();
                if (departamentos.Count == 0)
                {
                    Console.WriteLine("Cadastre um departamento antes de cadastrar funcionários.");
                    continue;
                }
                var func = new Funcionario();
                func.Id = Guid.NewGuid();
                Console.Write("Nome completo: ");
                func.NomeCompleto = Console.ReadLine() ?? "";
                Console.Write("Cargo: ");
                func.Cargo = Console.ReadLine() ?? "";
                Console.WriteLine("Escolha o departamento:");
                for (int i = 0; i < departamentos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {departamentos[i].NomeDepartamento} ({departamentos[i].Sigla})");
                }
                Console.Write("Número do departamento: ");
                if (int.TryParse(Console.ReadLine(), out int depIdx) && depIdx > 0 && depIdx <= departamentos.Count)
                {
                    func.DepartamentoId = departamentos[depIdx - 1].Id;
                    repoFunc.Adicionar(func);
                    Console.WriteLine("Funcionário cadastrado!");
                }
                else
                {
                    Console.WriteLine("Departamento inválido.");
                }
            }
            else if (opcao == "3")
            {
                var departamentos = repoDepto.ObterTodos();
                var funcionarios = repoFunc.ObterTodos();
                if (funcionarios.Count == 0)
                {
                    Console.WriteLine("Nenhum funcionário cadastrado.");
                    continue;
                }
                Console.WriteLine("--- Funcionários e seus Departamentos ---");
                foreach (var func in funcionarios)
                {
                    var depto = departamentos.FirstOrDefault(d => d.Id == func.DepartamentoId);
                    string nomeDepto = depto != null ? depto.NomeDepartamento : "Desconhecido";
                    Console.WriteLine($"{func.NomeCompleto} | {func.Cargo} | Departamento: {nomeDepto} ({func.DepartamentoId})");
                }
            }
            else if (opcao == "4")
            {
                var funcionarios = repoFunc.ObterTodos();
                if (funcionarios.Count == 0)
                {
                    Console.WriteLine("Nenhum funcionário para remover.");
                    continue;
                }
                Console.WriteLine("--- Funcionários ---");
                for (int i = 0; i < funcionarios.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {funcionarios[i].NomeCompleto} | {funcionarios[i].Cargo}");
                }
                Console.Write("Digite o número do funcionário para remover: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= funcionarios.Count)
                {
                    var func = funcionarios[idx - 1];
                    if (repoFunc.Remover(func.Id))
                        Console.WriteLine("Funcionário removido!");
                    else
                        Console.WriteLine("Erro ao remover funcionário.");
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