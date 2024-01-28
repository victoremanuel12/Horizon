using Horizon.Aplication.Mappings;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Aplication.Services;
using Horizon.Domain.Interfaces;
using Horizon.Infra.Data.Context;
using Horizon.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Horizon.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<IAirplaneService,AirplaneService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IClassTypeService, ClassTypeService>();
            services.AddScoped<IBuyService, BuyService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<IBuyerService, BuyerService>();
            services.AddScoped<IVoucherService, VoucherService>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();





            services.AddAutoMapper(typeof(DomainToDtoMappingProfile));

            return services;
        }
    }
}
