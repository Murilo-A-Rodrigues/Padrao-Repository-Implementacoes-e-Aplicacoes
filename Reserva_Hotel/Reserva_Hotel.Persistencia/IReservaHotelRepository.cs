using Hotel.Model;

namespace Hotel.Persistencia;

public interface IReservaHotelRepository : IRepository<ReservaHotel>
{
    IEnumerable<ReservaHotel> ObterPorStatus(StatusReserva status);
}