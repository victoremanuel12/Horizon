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

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<List<TicketDto>> BuyTicket(List<TicketDto> ticketDtoList)
        {
            var resultTicketList = new List<TicketDto>();

            try
            {
                foreach (var ticketDto in ticketDtoList)
                {
                    Class classSelected = await _unitOfWork.ClassRepository.GetByIdAsync(ticketDto.ClassId);

                    if (classSelected != null && classSelected.Seats > 0)
                    {
                        Ticket ticketEntity = _mapper.Map<Ticket>(ticketDto);

                        if (ticketDto.Dispatch)
                            ticketEntity.BaggageId = Guid.NewGuid();

                        classSelected.Seats -= 1;
                        _unitOfWork.ClassRepository.Update(classSelected);

                        await _unitOfWork.TicketRepository.CreateAsync(ticketEntity);
                        await _unitOfWork.Commit();

                        resultTicketList.Add(_mapper.Map<TicketDto>(ticketEntity));
                    }
                    else
                    {
                        resultTicketList.Add(new TicketDto());
                    }
                }

                return resultTicketList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public async Task<IEnumerable<TicketDto>> GetTicketByCpf(string cpf)
        {
            try
            {
                IEnumerable<Ticket> ticketsEntity = await _unitOfWork.TicketRepository.GetListByExpressionAsync(e => e.Cpf == cpf);
                if (!ticketsEntity.Any()) return new List<TicketDto>();

                IEnumerable<TicketDto> ticketsDtoResult = _mapper.Map<IEnumerable<TicketDto>>(ticketsEntity);
                return ticketsDtoResult;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }


    }
}
