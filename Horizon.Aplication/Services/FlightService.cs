using AutoMapper;
using CleanArchMvc.Domain.Validation;
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
        public async Task<IEnumerable<FlightDto>> GetAllFlights()
        {
            try
            {
                IEnumerable<Flight> flightsEntity = await _unitOfWork.FlightRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<FlightDto>>(flightsEntity);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<FlightDto> GetFlightById(Guid flightId)
        {
            try
            {

                Flight flightEntity = await _unitOfWork.FlightRepository.GetByIdAsync(flightId);
                return _mapper.Map<FlightDto>(flightEntity);
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


                return  _mapper.Map<FlightDto>(flightEntity);

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
