using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

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
        public async Task<Result<IEnumerable<CityDto>>> GetAllCities()
        {
            try
            {
                IEnumerable<City> citiesEntity = await _unitOfWork.CityRepository.GetAllAsync();
                if (citiesEntity is null)
                    return new Result<IEnumerable<CityDto>> { Success = false, ErrorMessage = "Nenhum voo foi encontrado", StatusCode = 404 };

                IEnumerable<CityDto> citiesDto = _mapper.Map<IEnumerable<CityDto>>(citiesEntity);
                return new Result<IEnumerable<CityDto>> { Success = true, Data = citiesDto, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<CityDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }
    }
}
