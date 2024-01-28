using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Domain;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

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
        public async Task<List<FlightWithNameAirportDto>> GetAllFlights()
        {
            try
            {
                IEnumerable<Flight> flightsEntity = await _unitOfWork.FlightRepository.GetAllAsync();
                List<FlightWithNameAirportDto> flightsWithNameDto = new List<FlightWithNameAirportDto>();

                foreach (var flight in flightsEntity)
                {
                    if (!flight.Canceled)
                    {
                        Airport originClassType = await _unitOfWork.AirportRepository.GetByIdAsync(flight.OriginId);
                        Airport destinyClassType = await _unitOfWork.AirportRepository.GetByIdAsync(flight.DestinyId);
                        FlightWithNameAirportDto flightDto = new FlightWithNameAirportDto
                        {
                            Id = flight.Id,
                            Code = flight.Code,
                            Time = flight.Time,
                            OriginId = originClassType.Id,
                            DestinyId = destinyClassType.Id,
                            NameOrigin = originClassType.Name,
                            NameDestiny = destinyClassType.Name,
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


        public async Task<FlightWithNameAirportDto> GetFlightById(Guid flightId)
        {
            try
            {

                Flight flightEntity = await _unitOfWork.FlightRepository.GetByIdAsync(flightId);
                Airport originClassType = await _unitOfWork.AirportRepository.GetByIdAsync(flightEntity.OriginId);
                Airport destinyClassType = await _unitOfWork.AirportRepository.GetByIdAsync(flightEntity.DestinyId);

                FlightWithNameAirportDto flightDtoResult = new FlightWithNameAirportDto
                {
                    Id = flightEntity.Id,
                    Code = flightEntity.Code,
                    Time = flightEntity.Time,
                    OriginId = originClassType.Id,
                    DestinyId = destinyClassType.Id,
                    NameOrigin = originClassType.Name,
                    NameDestiny = destinyClassType.Name,
                    Canceled = flightEntity.Canceled
                };
                return flightDtoResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
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


        public Task UpdateFlight(Guid flightId, FlightDto flightDto)
        {
            throw new NotImplementedException();
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

        public async Task<FlightDto> ModifyFlight(FlightDto flightDto)
        {
            try
            {
                Flight flightEntity = _mapper.Map<Flight>(flightDto);

                _unitOfWork.FlightRepository.Update(flightEntity);
                await _unitOfWork.Commit();

                return _mapper.Map<FlightDto>(flightEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
