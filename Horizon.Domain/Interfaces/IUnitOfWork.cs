using Horizon.Domain.Interfaces.Repositories;

namespace Horizon.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IAirportRepository AirportRepository { get; }
        IFlightRepository FlightRepository { get; }
        IClassRepository ClassRepository { get; }
        IClassTypeRepository ClassTypeRepository { get; }
        ITicketRepository TicketRepository { get; }
        IBuyRepository BuyRepository { get; }
        IVisitorRepository VisitorRepository { get; }
        IBuyerRepository BuyerRepository { get; }
        ILoginAdminRepository LoginAdminRepository { get; }
        Task Commit();
    }
}
