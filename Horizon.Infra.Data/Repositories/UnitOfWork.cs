﻿using Horizon.Domain.Interfaces;
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

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

    }
}
