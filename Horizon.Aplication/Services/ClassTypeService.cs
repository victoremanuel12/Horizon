using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class ClassTypeService : IClassTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ClassTypeDto>>> GetAllClassTypes()
        {
            try
            {
                IEnumerable<ClassType> classTypes = await _unitOfWork.ClassTypeRepository.GetAllAsync();
                if (classTypes is null)
                    return new Result<IEnumerable<ClassTypeDto>> { Success = false, ErrorMessage = "Os tipos de classe não foram encontradas ", StatusCode = 404 };
                IEnumerable<ClassTypeDto> classTypeDto = _mapper.Map<IEnumerable<ClassTypeDto>>(classTypes);
                return new Result<IEnumerable<ClassTypeDto>> { Success = true, Data = classTypeDto, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<ClassTypeDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }
    }
}
