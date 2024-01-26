using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Domain.Domain;
using Horizon.Domain.Entities;

namespace Horizon.Aplication.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Airport, AirportDto>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
            CreateMap<ClassType, ClassTypeDto>().ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
            //.ForMember(dest => dest.Classes, opt => opt.MapFrom(src => src.Class));
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<Class, ClassDto>().ReverseMap();

            
        }
    }
}
