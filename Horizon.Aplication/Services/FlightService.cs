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
        public async Task<Result<IEnumerable<FlightDetailsDto>>> GetAllFlights()
        {
            try
            {
                IEnumerable<Flight> flightsEntity = await _unitOfWork.FlightRepository.GetAllAsync();
                if (flightsEntity is null)
                    return new Result<IEnumerable<FlightDetailsDto>> { Success = false, ErrorMessage = "Nenhum voo foi encontrado", StatusCode = 404 };

                List<FlightDetailsDto> flightsDetailsDto = new List<FlightDetailsDto>();

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

                        flightsDetailsDto.Add(flightDto);
                    }

                }

                return new Result<IEnumerable<FlightDetailsDto>> { Success = true, Data = flightsDetailsDto, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<FlightDetailsDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
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
                    return new Result<FlightDetailsDto> { Success = false, ErrorMessage = "Não existem mais assentos disponíveis para este voo", StatusCode = 404 };

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
                return new Result<FlightDetailsDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }



        public async Task<Result<FlightDto>>  CreateFlight(FlightDto flightDto)
        {
            try
            {
                if (await HasSameCodeInDatabase(flightDto)) throw new Exception("O Código do voo já existe para em outro voo");
                if (await IsAirportInTheSameCity(flightDto)) throw new Exception("Os Aeroportos não podem estar na mesma cidade");

                Flight flightEntity = _mapper.Map<Flight>(flightDto);

                await _unitOfWork.FlightRepository.CreateAsync(flightEntity);
                await _unitOfWork.Commit();

                FlightDto flightDtoResult = _mapper.Map<FlightDto>(flightEntity);
                return new Result<FlightDto> { Success = true, Data = flightDtoResult, StatusCode = 201 };

            }
            catch (Exception ex)
            {
                return new Result<FlightDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
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




        public async Task<Result<bool>> CancelFlight(Guid flightId)
        {
            try
            {
                Flight flightEntity = await _unitOfWork.FlightRepository.GetByIdAsync(flightId);
                if (flightEntity is  null)
                    return new Result<bool> { Success = false, ErrorMessage = "Voo não encontrado", StatusCode = 404 };
                if (flightEntity != null)
                {
                    flightEntity.Canceled = true;
                    _unitOfWork.FlightRepository.Update(flightEntity);
                    await _unitOfWork.Commit();
                    return new Result<bool> { Success = true,SucessMessage = "Voo cancelado com sucesso",StatusCode = 200 };
                }
                 return new Result<bool> { Success = false,ErrorMessage = "Houve um erro ao cancelar o voo", StatusCode = 400 };
            }
            catch (Exception ex)
            {
                return new Result<bool> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }

        public async Task<Result<FlightDto>> UpdateFlight(Guid id, FlightDto flightDto)
        {
            try
            {
                Flight flightEntity = await _unitOfWork.FlightRepository.GetByIdAsync(id);
                if (flightEntity is null)
                    return new Result<FlightDto> { Success = false, ErrorMessage = "Voo não encontrado", StatusCode = 404 };
                Flight flightEntityUpdated = _mapper.Map<Flight>(flightDto);

                _unitOfWork.FlightRepository.Update(flightEntityUpdated);
                await _unitOfWork.Commit();
                FlightDto flightDtoResult =  _mapper.Map<FlightDto>(flightEntityUpdated);
                return  new Result<FlightDto> { Success = true, Data = flightDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<FlightDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }

    }
}
