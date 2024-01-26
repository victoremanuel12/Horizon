using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

namespace Horizon.Aplication.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClassService _classService;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketDto> BuyTicket(TicketDto ticketDto)
        {
            try
            {
                if(await hasSeatsToTravel(ticketDto))
                {

                }
               
                if (ticketDto.Dispatch) ticketDto.BaggageId = new Guid();
                Ticket ticketEntity = _mapper.Map<Ticket>(ticketDto);
                await _unitOfWork.TicketRepository.CreateAsync(ticketEntity);
                await _unitOfWork.Commit();
                return _mapper.Map<TicketDto>(ticketEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private async Task<bool> hasSeatsToTravel(TicketDto ticketDto)
        {
            Class classSelected = await _unitOfWork.ClassRepository.GetByIdAsync(ticketDto.ClassId);
            return classSelected.Seats > 0;
        }
    }
}
