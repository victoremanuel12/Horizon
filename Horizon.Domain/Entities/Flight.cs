using CleanArchMvc.Domain.Validation;
using Horizon.Domain.Entities;

namespace Horizon.Domain.Domain
{
    public class Flight : BaseEntity
    {
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public Guid OriginId { get; set; }
        public Guid DestinyId { get; set; }
        public bool Canceled { get; set; }

        public Airport Origin { get; set; }
        public Airport Destiny { get; set; }

        public ICollection<Class>? Classes { get; set; } = new HashSet<Class>();

        public Flight(string code, DateTime time, Guid originId, Guid destinyId, bool canceled)
        {
            ValidateDomain(code, time, originId, destinyId, canceled);
        }

        private void ValidateDomain(string code, DateTime time, Guid originId, Guid destinyId, bool canceled)
        {
            DomainExceptionValidation.When(originId == destinyId, "O destino e a origem do voo não podem ser o mesmo");
            Time = time;
            OriginId = originId;
            DestinyId = destinyId;
            Canceled = canceled;
            Code = code;
            Time = time;
            DestinyId = destinyId;
        }
    }

}
