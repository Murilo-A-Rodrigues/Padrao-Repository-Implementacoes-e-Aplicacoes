using Hotel.Model;
using Hotel.Persistencia;

class Program
{
    static void Main()
    {
        IReservaHotelRepository repo = new ReservaHotelJsonRepository();

        while (true)
        {
            Console.WriteLine("\n--- MENU RESERVAS DE HOTEL ---");
            Console.WriteLine("1 - Nova reserva");
            Console.WriteLine("2 - Listar todas as reservas");
            Console.WriteLine("3 - Listar reservas por status");
            Console.WriteLine("4 - Remover reserva");
            Console.WriteLine("5 - Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            if (opcao == "1")
            {
                var reserva = new ReservaHotel();
                reserva.Id = Guid.NewGuid();
                Console.Write("Nome do hóspede: ");
                reserva.NomeHospede = Console.ReadLine() ?? "";

                // Validação de datas
                DateTime entrada, saida;
                while (true)
                {
                    Console.Write("Data de entrada (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out entrada))
                    {
                        Console.WriteLine("Data inválida. Use o formato dd/MM/yyyy.");
                        continue;
                    }
                    if (entrada.Date < DateTime.Today)
                    {
                        Console.WriteLine("A data de entrada não pode ser no passado.");
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.Write("Data de saída (dd/MM/yyyy): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out saida))
                    {
                        Console.WriteLine("Data inválida. Use o formato dd/MM/yyyy.");
                        continue;
                    }
                    if (saida <= entrada)
                    {
                        Console.WriteLine("A data de saída deve ser após a data de entrada.");
                        continue;
                    }
                    break;
                }
                reserva.DataEntrada = entrada;
                reserva.DataSaida = saida;

                Console.WriteLine("Status da reserva:");
                foreach (var status in Enum.GetValues(typeof(StatusReserva)))
                    Console.WriteLine($"{(int)status} - {status}");
                Console.Write("Escolha o status (número): ");
                if (int.TryParse(Console.ReadLine(), out int statusInt) && Enum.IsDefined(typeof(StatusReserva), statusInt))
                    reserva.Status = (StatusReserva)statusInt;
                else
                    reserva.Status = StatusReserva.Pendente;

                repo.Adicionar(reserva);
                Console.WriteLine("Reserva cadastrada!");
            }
            else if (opcao == "2")
            {
                var reservas = repo.ObterTodos().ToList();
                if (reservas.Count == 0)
                {
                    Console.WriteLine("Nenhuma reserva cadastrada.");
                }
                else
                {
                    foreach (var r in reservas)
                    {
                        Console.WriteLine($"{r.NomeHospede} | Entrada: {r.DataEntrada:dd/MM/yyyy} | Saída: {r.DataSaida:dd/MM/yyyy} | Status: {r.Status} | ID: {r.Id}");
                    }
                }
            }
            else if (opcao == "3")
            {
                Console.WriteLine("Status disponíveis:");
                foreach (var status in Enum.GetValues(typeof(StatusReserva)))
                    Console.WriteLine($"{(int)status} - {status}");
                Console.Write("Digite o número do status: ");
                if (int.TryParse(Console.ReadLine(), out int statusInt) && Enum.IsDefined(typeof(StatusReserva), statusInt))
                {
                    var status = (StatusReserva)statusInt;
                    var reservas = repo.ObterPorStatus(status).ToList();
                    if (reservas.Count == 0)
                        Console.WriteLine("Nenhuma reserva com esse status.");
                    else
                        foreach (var r in reservas)
                            Console.WriteLine($"{r.NomeHospede} | Entrada: {r.DataEntrada:dd/MM/yyyy} | Saída: {r.DataSaida:dd/MM/yyyy} | Status: {r.Status} | ID: {r.Id}");
                }
                else
                {
                    Console.WriteLine("Status inválido.");
                }
            }
            else if (opcao == "4")
            {
                var reservas = repo.ObterTodos().ToList();
                if (reservas.Count == 0)
                {
                    Console.WriteLine("Nenhuma reserva para remover.");
                    continue;
                }
                for (int i = 0; i < reservas.Count; i++)
                {
                    var r = reservas[i];
                    Console.WriteLine($"{i + 1} - {r.NomeHospede} | Entrada: {r.DataEntrada:dd/MM/yyyy} | Saída: {r.DataSaida:dd/MM/yyyy} | Status: {r.Status}");
                }
                Console.Write("Digite o número da reserva para remover: ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= reservas.Count)
                {
                    var r = reservas[idx - 1];
                    if (repo.Remover(r.Id))
                        Console.WriteLine("Reserva removida!");
                    else
                        Console.WriteLine("Erro ao remover reserva.");
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