using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class BuyerService : IBuyerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BuyerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BuyerDto>> CreateNewBuyer(BuyerDto buyerDto)
        {
            try
            {
                Buyer buyerEntity = _mapper.Map<Buyer>(buyerDto);
                await _unitOfWork.BuyerRepository.CreateAsync(buyerEntity);
                await _unitOfWork.Commit();
                BuyerDto buyerDtoResult = _mapper.Map<BuyerDto>(buyerEntity);
                return new Result<BuyerDto> { Success = true, Data = buyerDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<BuyerDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }

        public async Task<Result<BuyerDto>> GetByerById(Guid id)
        {
            try
            {
                Buyer buyerFinded = await _unitOfWork.BuyerRepository.GetByIdAsync(id);
                if (buyerFinded == null)
                    return new Result<BuyerDto> { Success = false, ErrorMessage = "Comprador não encontrado", StatusCode = 404 };

                BuyerDto BuyerDtoResult = _mapper.Map<BuyerDto>(buyerFinded);
                return new Result<BuyerDto> { Success = true, Data = BuyerDtoResult, StatusCode = 404 };
            }
            catch (Exception ex)
            {
                return new Result<BuyerDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };

            }

        }
    }
}
