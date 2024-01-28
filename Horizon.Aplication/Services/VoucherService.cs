using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Domain;
using Horizon.Domain.Entities;
using Horizon.Domain.Interfaces;

namespace Horizon.Aplication.Services
{
    public class VoucherService : IVoucherService
    {

        private readonly IUnitOfWork _unitOfWork;

        public VoucherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VoucherDto> GenereteVoucher(Guid IdTicket)
        {
            try
            {
                var datavoucherSelect =   _unitOfWork.TicketRepository.SelectIncludes(
                    t => t.Id == IdTicket,
                    c => c.Class, 
                    f => f.Class.Flight,
                    d => d.Class.Flight.Destiny,
                    o => o.Class.Flight.Origin 
                ); 

                if (datavoucherSelect is null)
                    return new VoucherDto();
                var voucher = new VoucherDto
                {
                    IdTicket = datavoucherSelect[0].Id,
                    PassengerName = datavoucherSelect[0].Name,
                    PassengerCpf = datavoucherSelect[0].Cpf,
                    Dispatch = datavoucherSelect[0].Dispatch,
                    IdFlight = datavoucherSelect[0].Class.FlightId,
                    Origin = datavoucherSelect[0].Class.Flight.Origin.Name,
                    Destiny = datavoucherSelect[0].Class.Flight.Destiny.Name,

                };
                return voucher;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }

        }
    }
}
