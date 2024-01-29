using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

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

        public async Task<Result<VisitorDto>> CreateNewVisitor(VisitorDto visitorDto)
        {
            try
            {
                Visitor visitorEntity = _mapper.Map<Visitor>(visitorDto);
                await _unitOfWork.VisitorRepository.CreateAsync(visitorEntity);
                await _unitOfWork.Commit();
                VisitorDto visitorDtoResult = _mapper.Map<VisitorDto>(visitorEntity);
                return new Result<VisitorDto> { Success = true, Data = visitorDtoResult, StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return new Result<VisitorDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };

            }
        }

        public async Task<Result<VisitorDto>> GetVisitorById(Guid id)
        {
            try
            {

                Visitor visitorEntity = await _unitOfWork.VisitorRepository.GetByIdAsync(id);
                if (visitorEntity == null)
                    return new Result<VisitorDto> { Success = false, ErrorMessage = "Dados do visitante não encontrados", StatusCode = 404 };
                VisitorDto visitorDtoResult = _mapper.Map<VisitorDto>(visitorEntity);
                return new Result<VisitorDto> { Success = true, Data = visitorDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<VisitorDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }

        }
    }
}
