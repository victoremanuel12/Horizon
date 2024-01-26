using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

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

        public async Task<IEnumerable<ClassTypeDto>> GetAllClassTypes()
        {
            try 
            {
                IEnumerable<ClassType> classTypes = await _unitOfWork.ClassTypeRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ClassTypeDto>>(classTypes);
            }catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
