﻿using Horizon.Domain.Domain;
using Horizon.Domain.Entities;
using Horizon.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Horizon.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Airport>(new AirportMap().Configure);
            builder.Entity<Buy>(new BuyMap().Configure);
            builder.Entity<Buyer>(new BuyerMaper().Configure);
            builder.Entity<City>(new CityMap().Configure);
            builder.Entity<Class>(new ClassMap().Configure);
            builder.Entity<ClassType>(new ClassTypeMap().Configure);
            builder.Entity<Flight>(new FlightMap().Configure);
            builder.Entity<Login>(new LoginMap().Configure);
            builder.Entity<Ticket>(new TicketMap().Configure);
            builder.Entity<Visitor>(new VisitorMap().Configure);






            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
