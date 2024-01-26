using CleanArchMvc.Domain.Validation;
using Horizon.Domain.Domain;
using System.Diagnostics;

namespace Horizon.Domain.Entities
{
    public class Class : BaseEntity
    {

        public Guid ClassTypeId { get; set; }
        public Guid FlightId { get; set; }
        public int Seats { get; set; }
        public decimal Price { get; set; }

        public ClassType ClassType { get; set; }
        public Flight Flight { get; set; }

        public Class(Guid classTypeId, Guid flightId, int seats, decimal price)
        {
            ValidateDomain(classTypeId, flightId, seats, price);


        }
        private void ValidateDomain(Guid classTypeId, Guid flightId, int seats, decimal price)
        {
            DomainExceptionValidation.When(seats <= 0, "O voo deve te no minimo um assento");
            DomainExceptionValidation.When(price <= 0, "O valor do assento deve ser informado");
            ClassTypeId = classTypeId;
            FlightId = flightId;
            Seats = seats;
            Price = price;
        }

    }

}
