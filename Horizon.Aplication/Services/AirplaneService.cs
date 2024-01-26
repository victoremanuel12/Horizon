using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Domain;
using Horizon.Domain.Interfaces;

namespace Horizon.Aplication.Services
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AirplaneService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AirportDto>> GetAllFlights()
        {
            try
            {
                IEnumerable<Airport> airports = await _unitOfWork.AirportRepository.GetAllAsync();
                IEnumerable<AirportDto> airportDto = _mapper.Map<IEnumerable<AirportDto>>(airports);
                return airportDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
