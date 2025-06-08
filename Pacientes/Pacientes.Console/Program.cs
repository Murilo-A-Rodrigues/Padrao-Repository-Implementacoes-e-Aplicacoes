using Pacientes.Model;
using Pacientes.Persistencia;

class Program
{
    static void Main()
    {
        IPacienteRepository repo = new PacienteJsonRepository();

        while (true)
        {
            Console.WriteLine("\n--- MENU PACIENTES ---");
            Console.WriteLine("1 - Cadastrar paciente");
            Console.WriteLine("2 - Listar todos os pacientes");
            Console.WriteLine("3 - Buscar pacientes por faixa etária");
            Console.WriteLine("4 - Excluir paciente");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var paciente = new Paciente();
                paciente.Id = Guid.NewGuid();
                Console.Write("Nome completo: ");
                paciente.NomeCompleto = Console.ReadLine() ?? "";

                // Validação da data de nascimento
                DateTime dataNasc;
                while (true)
                {
                    Console.Write("Data de nascimento (dd/MM/yyyy): ");
                    var entrada = Console.ReadLine();
                    if (!DateTime.TryParse(entrada, out dataNasc))
                    {
                        Console.WriteLine("Data inválida. Tente novamente.");
                        continue;
                    }
                    if (dataNasc > DateTime.Today)
                    {
                        Console.WriteLine("A data de nascimento não pode ser no futuro.");
                        continue;
                    }
                    if (dataNasc.Year < 1900)
                    {
                        Console.WriteLine("Ano de nascimento muito antigo. Tente novamente.");
                        continue;
                    }
                    break;
                }
                paciente.DataNascimento = dataNasc;

                Console.Write("Contato de emergência: ");
                paciente.ContatoEmergencia = Console.ReadLine() ?? "";
                repo.Adicionar(paciente);
                Console.WriteLine("Paciente cadastrado!");
            }
            else if (opcao == "2")
            {
                Console.WriteLine("\n--- Lista de Pacientes ---");
                var pacientes = repo.ObterTodos().ToList();
                if (pacientes.Count == 0)
                {
                    Console.WriteLine("Nenhum paciente cadastrado.");
                }
                else
                {
                    foreach (var p in pacientes)
                    {
                        var (anos, meses) = CalcularIdadeAnosMeses(p.DataNascimento);
                        Console.WriteLine($"{p.NomeCompleto} | Nascimento: {p.DataNascimento:dd/MM/yyyy} | Idade: {anos} anos e {meses} meses | Contato: {p.ContatoEmergencia} | ID: {p.Id}");
                    }
                }
            }
            else if (opcao == "3")
            {
                Console.Write("Idade mínima: ");
                int.TryParse(Console.ReadLine(), out int min);
                Console.Write("Idade máxima: ");
                int.TryParse(Console.ReadLine(), out int max);
                var pacientes = repo.ObterPorFaixaEtaria(min, max).ToList();
                if (pacientes.Count == 0)
                {
                    Console.WriteLine("Nenhum paciente encontrado na faixa etária.");
                }
                else
                {
                    Console.WriteLine("\n--- Pacientes na faixa etária ---");
                    foreach (var p in pacientes)
                    {
                        var (anos, meses) = CalcularIdadeAnosMeses(p.DataNascimento);
                        Console.WriteLine($"{p.NomeCompleto} | Nascimento: {p.DataNascimento:dd/MM/yyyy} | Idade: {anos} anos e {meses} meses | Contato: {p.ContatoEmergencia} | ID: {p.Id}");
                    }
                }
            }
            else if (opcao == "4")
            {
                var pacientes = repo.ObterTodos().ToList();
                if (pacientes.Count == 0)
                {
                    Console.WriteLine("Nenhum paciente para remover.");
                    continue;
                }
                Console.WriteLine("--- Pacientes ---");
                for (int i = 0; i < pacientes.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {pacientes[i].NomeCompleto} | {pacientes[i].DataNascimento:dd/MM/yyyy}");
                }
                Console.Write("Digite o número do paciente para remover: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= pacientes.Count)
                {
                    var paciente = pacientes[idx - 1];
                    if (repo.Remover(paciente.Id))
                        Console.WriteLine("Paciente removido!");
                    else
                        Console.WriteLine("Erro ao remover paciente.");
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

    static (int anos, int meses) CalcularIdadeAnosMeses(DateTime dataNascimento)
    {
        var hoje = DateTime.Today;
        int anos = hoje.Year - dataNascimento.Year;
        int meses = hoje.Month - dataNascimento.Month;
        if (hoje.Day < dataNascimento.Day)
            meses--;
        if (meses < 0)
        {
            anos--;
            meses += 12;
        }
        if (anos < 0) anos = 0;
        if (meses < 0) meses = 0;
        return (anos, meses);
    }
}