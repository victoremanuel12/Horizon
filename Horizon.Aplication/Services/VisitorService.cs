using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

namespace Horizon.Aplication.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VisitorDto> CreateNewVisitor(VisitorDto visitorDto)
        {
            try
            {
                Visitor visitorEntity = _mapper.Map<Visitor>(visitorDto);
                await _unitOfWork.VisitorRepository.CreateAsync(visitorEntity);
                await _unitOfWork.Commit();
                VisitorDto visitorDtoResult = _mapper.Map<VisitorDto>(visitorEntity);
                return visitorDtoResult;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }

        public async Task<VisitorDto> GetVisitorById(Guid id)
        {
            try
            {

                Visitor visitorEntity = await _unitOfWork.VisitorRepository.GetByIdAsync(id);
                return _mapper.Map<VisitorDto>(visitorEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
