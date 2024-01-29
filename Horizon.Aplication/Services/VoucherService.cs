using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;

namespace Horizon.Aplication.Services
{
    public class VoucherService : IVoucherService
    {

        private readonly IUnitOfWork _unitOfWork;

        public VoucherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VoucherDto>> GenerateVoucher(Guid IdTicket)
        {
            try
            {
                var datavoucherSelect = _unitOfWork.TicketRepository.SelectIncludes(
                    t => t.Id == IdTicket,
                    c => c.Class,
                    f => f.Class.Flight,
                    d => d.Class.Flight.Destiny,
                    o => o.Class.Flight.Origin
                );

                if (datavoucherSelect == null || datavoucherSelect.Count == 0)
                    return new Result<VoucherDto> { Success = false, ErrorMessage = "Erro ao encontrar dados da passagem", StatusCode = 404 };

                var ticket = datavoucherSelect[0];
                var flightDateTime = ticket.Class.Flight.Time;
                var currentDateTime = DateTime.Now;
                var hoursDifference = (flightDateTime - currentDateTime).TotalHours;

                if (hoursDifference >= 5 && flightDateTime.Date != currentDateTime.Date)
                    return new Result<VoucherDto> { Success = false, ErrorMessage = "O voucher só pode ser emitido 5 horas antes do voo", StatusCode = 400 };

                var voucherGenerate = new VoucherDto
                {
                    IdTicket = ticket.Id,
                    PassengerName = ticket.Name,
                    PassengerCpf = ticket.Cpf,
                    Dispatch = ticket.Dispatch,
                    IdFlight = ticket.Class.FlightId,
                    Origin = ticket.Class.Flight.Origin?.Name, 
                    Destiny = ticket.Class.Flight.Destiny?.Name, 
                };

                return new Result<VoucherDto> { Success = true, Data = voucherGenerate, StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new Result<VoucherDto> { Success = false, ErrorMessage = "Ocorreu um erro ao gerar o voucher.", StatusCode = 500 };
            }
        }







    }
}
