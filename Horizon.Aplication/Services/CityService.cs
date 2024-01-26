using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

namespace Horizon.Aplication.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CityDto>> GetAllCities()
        {
            try
            {
                IEnumerable<City> citiesEntity = await _unitOfWork.CityRepository.GetAllAsync();
                IEnumerable<CityDto> citiesDto = _mapper.Map<IEnumerable<CityDto>>(citiesEntity);

                return citiesDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
