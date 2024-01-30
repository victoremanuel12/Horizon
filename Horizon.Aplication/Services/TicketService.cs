using AutoMapper;
using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

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


        public async Task<Result<List<TicketDto>>> BuyTickets(List<TicketDto> ticketDtoList)
        {
            var resultTicketList = new List<TicketDto>();

            try
            {
                foreach (var ticketDto in ticketDtoList)
                {
                    Class classSelected = await _unitOfWork.ClassRepository.GetByIdAsync(ticketDto.ClassId);

                    if (classSelected == null || classSelected.Seats == classSelected.OccupiedSeat)
                        throw new Exception("Não existem mais passagens para essa classe");


                    Ticket ticketEntity = _mapper.Map<Ticket>(ticketDto);

                    ticketEntity.Price = classSelected.Price;
                    classSelected.OccupiedSeat += 1;
                    ticketEntity.Price = ticketEntity.Dispatch ? classSelected.Price * 1.1m : classSelected.Price;

                    _unitOfWork.ClassRepository.Update(classSelected);
                    await _unitOfWork.TicketRepository.CreateAsync(ticketEntity);
                    await _unitOfWork.Commit();

                    resultTicketList.Add(_mapper.Map<TicketDto>(ticketEntity));

                }

                return new Result<List<TicketDto>> { Success = true, Data = resultTicketList, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<List<TicketDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }
        }


        public async Task<Result<IEnumerable<TicketDto>>> GetTicketByCpf(string cpf)
        {
            try
            {
                IEnumerable<Ticket> ticketsEntity = await _unitOfWork.TicketRepository.GetListByExpressionAsync(e => e.Cpf == cpf);
                if (!ticketsEntity.Any())
                    return new Result<IEnumerable<TicketDto>> { Success = false, ErrorMessage = "Nenhuma passagem foi encontrada para esse CPF", StatusCode = 404 };

                IEnumerable<TicketDto> ticketsDtoResult = _mapper.Map<IEnumerable<TicketDto>>(ticketsEntity);

                return new Result<IEnumerable<TicketDto>> { Success = true, Data = ticketsDtoResult, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<TicketDto>> { Success = false, ErrorMessage = $"{ex.Message}", StatusCode = 400 };
            }

        }
    }
}


