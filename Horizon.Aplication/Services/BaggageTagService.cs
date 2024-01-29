using Horizon.Aplication.Dtos;
using Horizon.Aplication.ServiceInterfaces;
using Horizon.Domain.Interfaces;
using static Horizon.Domain.Validation.ErroResultOperation;
namespace Horizon.Aplication.Services
{
    public class BaggageTagService : IBaggageTagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaggageTagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<BaggageTagDto>> GenerateBaggageTag(Guid idTicket)
        {
            var dataBaggageSelect = _unitOfWork.TicketRepository.SelectIncludes(
                 t => t.Id == idTicket,
                 c => c.Class,
                 f => f.Class.Flight
             );

            if (dataBaggageSelect is null || !dataBaggageSelect[0].Dispatch)
                return new Result<BaggageTagDto> { Success = false, ErrorMessage = "A bagagem não foi despachada para ser emitida a Etiqueta", StatusCode = 400 };

            DateTime flightDateTime = dataBaggageSelect[0].Class.Flight.Time;
            DateTime currentDateTime = DateTime.Now;
            double hoursDifference = (flightDateTime - currentDateTime).TotalHours;

            if (hoursDifference >= 5 && flightDateTime.Date != currentDateTime.Date)
                return new Result<BaggageTagDto> { Success = false, ErrorMessage = "A etiqueta só pode ser emitido 5 horas antes do voo", StatusCode = 400 };

            BaggageTagDto baggageTag = new BaggageTagDto
            {
                TicketId = dataBaggageSelect[0].Id,
                BaggageTag = dataBaggageSelect[0].BaggageId,
                passengerName = dataBaggageSelect[0].Name
            };

            return new Result<BaggageTagDto> { Success = true, ErrorMessage = null, StatusCode = 200, Data = baggageTag };

        }
    }
}
