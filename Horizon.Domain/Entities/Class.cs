using CleanArchMvc.Domain.Validation;
using Horizon.Domain.Domain;
using System.Diagnostics;

namespace Horizon.Domain.Entities
{
    public sealed class Class : BaseEntity
    {

        public Guid ClassTypeId { get;  set; }
        public Guid FlightId { get;  set; }
        public int Seats { get;  set; }
        public decimal Price { get;  set; }
        public int OccupiedSeat { get;  set; }
        public ClassType ClassType { get;  set; }
        public Flight Flight { get;  set; }

        public Class(Guid classTypeId, Guid flightId, int seats, decimal price, int occupiedSeat)
        {
            ValidateDomain(classTypeId,flightId, seats, price, occupiedSeat);

        }
        private void ValidateDomain(Guid classTypeId, Guid flightId, int seats, decimal price, int occupiedSeat)
        {
            DomainExceptionValidation.When(seats < 0, "O cadastro da classe deve te no minimo um assento ou não existem mais assentos nessa classe");
            DomainExceptionValidation.When(price < 0, "O valor do assento deve ser informado");
            ClassTypeId = classTypeId;
            FlightId = flightId;
            Seats = seats;
            Price = price;
            OccupiedSeat = occupiedSeat;
        }

    }

}
