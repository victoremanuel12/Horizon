using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Domain;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FlightService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<FlightDetailsDto>> GetAllFlights()
        {
            try
            {
                IEnumerable<Flight> flightsEntity = await _unitOfWork.FlightRepository.GetAllAsync();


                List<FlightDetailsDto> flightsWithNameDto = new List<FlightDetailsDto>();

                foreach (var flight in flightsEntity)
                {
                    var flightWithIncludes = _unitOfWork.FlightRepository.SelectIncludes(f => f.Id == flight.Id, d => d.Destiny, o => o.Origin);

                    if (!flight.Canceled)
                    {

                        FlightDetailsDto flightDto = new FlightDetailsDto
                        {
                            Id = flight.Id,
                            Code = flight.Code,
                            Time = flight.Time,
                            OriginId = flightWithIncludes[0].OriginId,
                            DestinyId = flightWithIncludes[0].DestinyId,
                            NameOrigin = flightWithIncludes[0].Origin.Name,
                            NameDestiny = flightWithIncludes[0].Destiny.Name,
                            Canceled = flight.Canceled
                        };

                        flightsWithNameDto.Add(flightDto);
                    }

                }

                return flightsWithNameDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public async Task<Result<FlightDetailsDto>> GetFlightById(Guid flightId)
        {
            try
            {
                var flight = _unitOfWork.FlightRepository
                    .SelectIncludes(f => f.Id == flightId, d => d.Destiny, o => o.Origin, c => c.Classes)
                    .FirstOrDefault();

                if (flight == null)
                    return new Result<FlightDetailsDto> { Success = false, ErrorMessage = "Dados do Voo não encontrados", StatusCode = 404 };


                var availableClasses = flight.Classes.Where(item => item.OccupiedSeat < item.Seats).ToList();

                if (availableClasses.Count == 0)
                    return new Result<FlightDetailsDto> { Success = false, ErrorMessage = "Não existem mais assentos disponíveis para este voo", StatusCode = 400 };

                List<ClassDto> classDtoList = _mapper.Map<List<ClassDto>>(availableClasses);

                var flightDetailsDto = new FlightDetailsDto
                {
                    Id = flight.Id,
                    Code = flight.Code,
                    Time = flight.Time,
                    OriginId = flight.OriginId,
                    DestinyId = flight.DestinyId,
                    NameOrigin = flight.Origin?.Name,
                    NameDestiny = flight.Destiny?.Name,
                    Canceled = flight.Canceled,
                    Classes = classDtoList
                };

                return new Result<FlightDetailsDto> { Success = true, Data = flightDetailsDto, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<FlightDetailsDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 500 };
            }
        }



        public async Task<FlightDto> CreateFlight(FlightDto flightDto)
        {
            try
            {
                if (await HasSameCodeInDatabase(flightDto)) throw new Exception("O Código do voo já existe para em outro voo");
                if (await IsAirportInTheSameCity(flightDto)) throw new Exception("Os Aeroportos não podem estar na mesma cidade");

                Flight flightEntity = _mapper.Map<Flight>(flightDto);

                await _unitOfWork.FlightRepository.CreateAsync(flightEntity);
                await _unitOfWork.Commit();


                return _mapper.Map<FlightDto>(flightEntity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private async Task<bool> HasSameCodeInDatabase(FlightDto flightDto)
        {
            try
            {
                Flight existingFlight = await _unitOfWork.FlightRepository.GetByExpressionAsync(f => f.Code == flightDto.Code);
                return existingFlight != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private async Task<bool> IsAirportInTheSameCity(FlightDto flightDto)
        {
            try
            {
                Airport AirportOrigin = await _unitOfWork.AirportRepository.GetByIdAsync(flightDto.OriginId);
                Airport AirportDestiny = await _unitOfWork.AirportRepository.GetByIdAsync(flightDto.DestinyId);

                if (AirportOrigin == null || AirportDestiny == null) throw new NullReferenceException("Dados do aeroporto de destino ou origem não existe");

                return AirportOrigin.CityId == AirportDestiny.CityId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }




        public async Task<bool> CancelFlight(Guid flightId)
        {
            try
            {
                Flight flightEntity = await _unitOfWork.FlightRepository.GetByIdAsync(flightId);

                if (flightEntity != null)
                {
                    flightEntity.Canceled = true;
                    _unitOfWork.FlightRepository.Update(flightEntity);
                    await _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<FlightDto> UpdateFlight(Guid id, FlightDto flightDto)
        {
            try
            {
                Flight flightEntity = await _unitOfWork.FlightRepository.GetByIdAsync(id);
                Flight flightEntityUpdated = _mapper.Map<Flight>(flightDto);

                _unitOfWork.FlightRepository.Update(flightEntity);
                await _unitOfWork.Commit();
                return _mapper.Map<FlightDto>(flightEntityUpdated);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
