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

        public List<Class> Classes { get; set; } 

      

        public Flight(Guid id,string code, DateTime time, Guid originId, Guid destinyId, bool canceled)
        {
            ValidateDomain(id, code, time, originId, destinyId, canceled);
      

        }

        private void ValidateDomain(Guid id, string code, DateTime time, Guid originId, Guid destinyId, bool canceled)
        {
            DomainExceptionValidation.When(originId == destinyId, "O destino e a origem do voo não podem ser o mesmo");
            DomainExceptionValidation.When(string.IsNullOrEmpty(code), "Código do voo é obrigatório");
            DomainExceptionValidation.When(time == DateTime.MinValue, "Data do voo não pode ser vazia");
            Id = id;
            Code = code;
            Time = time;
            OriginId = originId;
            DestinyId = destinyId;
            Canceled = canceled;
        }
    }

}
