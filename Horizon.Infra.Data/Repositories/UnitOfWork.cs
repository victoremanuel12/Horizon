using Horizon.Domain.Interfaces;
using Horizon.Domain.Interfaces.Repositories;
using Horizon.Infra.Data.Context;

namespace Horizon.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CityRepository _cityRepository;
        private AirportRepository _airportRepository;
        private FlightRepository _flightRepository;
        private ClassRepository _classRepository;
        private ClassTypeRepository _classTypeRepository;
        private TicketRepository _ticketRepository;
        private BuyRepository _buyRepository;
        private VisitorRepository _visitorRepository;
        private BuyerRepository _buyerRepository;
        public ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICityRepository CityRepository
        {
            get
            {
                return _cityRepository = _cityRepository ?? new CityRepository(_context);
            }
        }

        public IAirportRepository AirportRepository
        {

            get
            {
                return _airportRepository = _airportRepository ?? new AirportRepository(_context);
            }
        }
        public IFlightRepository FlightRepository
        {

            get
            {
                return _flightRepository = _flightRepository ?? new FlightRepository(_context);
            }
        }
        public IClassRepository ClassRepository
        {

            get
            {
                return _classRepository = _classRepository ?? new ClassRepository(_context);
            }
        }
        public IClassTypeRepository ClassTypeRepository
        {

            get
            {
                return _classTypeRepository = _classTypeRepository ?? new ClassTypeRepository(_context);
            }
        }
        public ITicketRepository TicketRepository
        {

            get
            {
                return _ticketRepository = _ticketRepository ?? new TicketRepository(_context);
            }
        }
        public IBuyRepository BuyRepository
        {

            get
            {
                return _buyRepository = _buyRepository ?? new BuyRepository(_context);
            }
        }
        public IVisitorRepository VisitorRepository
        {

            get
            {
                return _visitorRepository = _visitorRepository ?? new VisitorRepository(_context);
            }
        }
        public IBuyerRepository BuyerRepository
        {

            get
            {
                return _buyerRepository = _buyerRepository ?? new BuyerRepository(_context);
            }
        }


        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

    }
}
