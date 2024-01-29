using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Domain;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class AirportService : IAirportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AirportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        async Task<Result<IEnumerable<AirportDto>>> IAirportService.GetAllFlights()
        {
            try
            {
                IEnumerable<Airport> airports = await _unitOfWork.AirportRepository.GetAllAsync();
                if (airports is null)
                    return new Result<IEnumerable<AirportDto>> { Success = false, ErrorMessage = "Nenhum voo foi encontrado", StatusCode = 404 };
                IEnumerable<AirportDto> airportsDto = _mapper.Map<IEnumerable<AirportDto>>(airports);
                return new Result<IEnumerable<AirportDto>> { Success = true, Data = airportsDto, StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<AirportDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 404 };
            }

        }


    }
}
