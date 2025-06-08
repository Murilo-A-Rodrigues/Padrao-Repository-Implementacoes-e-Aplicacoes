namespace Hotel.Model;

public class ReservaHotel : IEntidade
{
    public Guid Id { get; set; }
    public string NomeHospede { get; set; } = string.Empty;
    public DateTime DataEntrada { get; set; }
    public DateTime DataSaida { get; set; }
    public StatusReserva Status { get; set; }
}