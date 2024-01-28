using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

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

        public async Task<BuyerDto> CreateNewBuyer(BuyerDto buyerDto)
        {
            try
            {
                Buyer buyerEntity = _mapper.Map<Buyer>(buyerDto);
                await _unitOfWork.BuyerRepository.CreateAsync(buyerEntity);
                await _unitOfWork.Commit();
                return _mapper.Map<BuyerDto>(buyerEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<BuyerDto> GetByerById(Guid id)
        {
            try
            {
                Buyer buyerFinded = await _unitOfWork.BuyerRepository.GetByIdAsync(id);
                if(buyerFinded == null) 
                    return new BuyerDto();
                return _mapper.Map<BuyerDto>(buyerFinded);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }

        }
    }
}
