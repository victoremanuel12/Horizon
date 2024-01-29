using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using Horizon.Domain.Interfaces.Repositories;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class BuyService : IBuyService
    {
        private readonly ITicketService _ticketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BuyService(IMapper mapper, ITicketService ticketService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _ticketService = ticketService;
            _unitOfWork = unitOfWork;
        }



        public async Task<Result<BuyDto>> OrderBuyTikets(BuyDto buyDto)
        {
            try
            {

                Buy orderBuyEntity = _mapper.Map<Buy>(buyDto);
                Result<List<TicketDto>> tiketsBought = await _ticketService.BuyTickets(buyDto.Tickets);
                if (tiketsBought.Data == null)
                    throw new Exception(tiketsBought.ErrorMessage);

                await _unitOfWork.BuyRepository.CreateAsync(orderBuyEntity);
                await _unitOfWork.Commit();
                BuyDto buyDtoResult = _mapper.Map<BuyDto>(orderBuyEntity);
                buyDtoResult.Tickets = tiketsBought.Data;
                return new Result<BuyDto> { Success = true, Data = buyDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<BuyDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };

            }
        }
        public async Task<Result<BuyDto>> CancelBuy(Guid idBuy)
        {
            try
            {
                Buy buyEntity = await _unitOfWork.BuyRepository.GetByIdAsync(idBuy);
                if (buyEntity is null)
                    return new Result<BuyDto> { Success = false, ErrorMessage = "Dados da compra não encontrados", StatusCode = 404 };

                await CancelTickets(buyEntity.Id);
                buyEntity.Canceled = true;

                _unitOfWork.BuyRepository.Update(buyEntity);
                await _unitOfWork.Commit();
                BuyDto buyDtoResult = _mapper.Map<BuyDto>(buyEntity);

                return new Result<BuyDto> { Success = true, Data = buyDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<BuyDto> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };

            }
        }
        private async Task CancelTickets(Guid idBuy)
        {
            var ticketsOfBuyCanceledEntity = await _unitOfWork.TicketRepository.GetListByExpressionAsync(e => e.BuyId == idBuy);
            if (ticketsOfBuyCanceledEntity == null)
                throw new InvalidOperationException("Erro ao cancelar passagens vinculadas a compra");
            foreach (var ticket in ticketsOfBuyCanceledEntity)
            {
                if (ticket != null)
                {
                    ticket.Canceled = true;
                    _unitOfWork.TicketRepository.Update(ticket);
                }
            }
        }
    }
}
