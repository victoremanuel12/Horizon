using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using Horizon.Domain.Interfaces.Repositories;

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



        public async Task<BuyDto> OrderBuyTikets(BuyDto buyDto)
        {
            try
            {

                Buy orderBuyEntity = _mapper.Map<Buy>(buyDto);
                await _unitOfWork.BuyRepository.CreateAsync(orderBuyEntity);
                await _unitOfWork.Commit();
                List<TicketDto> tiketsBought = await _ticketService.BuyTickets(buyDto.Tickets);

                BuyDto buyDtoResult = _mapper.Map<BuyDto>(orderBuyEntity);
                buyDtoResult.Tickets = tiketsBought;
                return buyDtoResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }
        public async Task<BuyDto> CancelBuy(Guid idBuy)
        {
            try
            {
                Buy buyEntity = await _unitOfWork.BuyRepository.GetByIdAsync(idBuy);
                if (buyEntity is null)
                    return new BuyDto();

                buyEntity.Canceled = true;
                _unitOfWork.BuyRepository.Update(buyEntity);

                await CancelTickets(buyEntity.Id);

                await _unitOfWork.Commit();
                return _mapper.Map<BuyDto>(buyEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
        }
        private async Task CancelTickets(Guid idBuy)
        {
            var ticketsOfBuyCanceledEntity = await _unitOfWork.TicketRepository.GetListByExpressionAsync(e => e.BuyId == idBuy);
            if (ticketsOfBuyCanceledEntity != null)
            {
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
}
