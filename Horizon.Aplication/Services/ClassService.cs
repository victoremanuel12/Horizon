using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

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

        public async Task<ClassDto> ChangeSeatsPrice(Guid classId, ClassDto classDto)
        {
            try
            {
                Class classEntity = await _unitOfWork.ClassRepository.GetByIdAsync(classId);
                _mapper.Map(classDto, classEntity);
                _unitOfWork.ClassRepository.Update(classEntity);
                await _unitOfWork.Commit();
                return _mapper.Map<ClassDto>(classEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

       
    }
}
