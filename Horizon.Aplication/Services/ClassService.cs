using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ClassDto>> ChangeSeatsPrice(Guid classId, ChangeSeatsPriceDto changePriceDto)
        {
            try
            {
                Class classEntity = await _unitOfWork.ClassRepository.GetByIdAsync(classId);
                if (classEntity == null)
                    return new Result<ClassDto> { Success = false, ErrorMessage = $"Class do voo não encontrada", StatusCode = 404 };

                classEntity.Price = changePriceDto.Price;
                await _unitOfWork.Commit();
                ClassDto classDtoResult = _mapper.Map<ClassDto>(classEntity);
                return new Result<ClassDto> { Success = true, Data = classDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<ClassDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }

        }

        public async Task<Result<List<ClassDto>>> CreateClassToFlight(List<ClassDto> classes)
        {
            try
            {
                HasduplicateClassTypes(classes);
                List<ClassDto> classDtoResult = new List<ClassDto>();

                foreach (var classItem in classes)
                {
                    await HasFlightWithSameClass(classItem);
                    Class classEntity = _mapper.Map<Class>(classItem);
                    await _unitOfWork.ClassRepository.CreateAsync(classEntity);
                    await _unitOfWork.Commit();
                    ClassDto createdClassDto = _mapper.Map<ClassDto>(classEntity);
                    classDtoResult.Add(createdClassDto);
                }

                return new Result<List<ClassDto>> { Success = true, Data = classDtoResult, StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return new Result<List<ClassDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };

            }
        }

        private async Task HasFlightWithSameClass(ClassDto classDto)
        {

            var classOfFlight = await _unitOfWork.ClassRepository.GetListByExpressionAsync(e => e.FlightId == classDto.FlightId);
            if (classOfFlight.Count() > 0)
            {
                foreach (var classItem in classOfFlight)
                {
                    if (classItem.ClassTypeId == classDto.ClassTypeId)
                        throw new InvalidOperationException("Esse voo já está cadastrado com essa(s) classe(s)");
                }
            }


        }
        private void HasduplicateClassTypes(List<ClassDto> classDtoList)
        {
            var duplicateClassTypes = classDtoList
            .GroupBy(c => c.ClassTypeId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

            if (duplicateClassTypes.Any())
                throw new InvalidOperationException("Não é permitido um voo com duas classes iguais");
        }
    }
}
