using System.Diagnostics;
using System.Xml.Linq;
using System;
using CleanArchMvc.Domain.Validation;

namespace Horizon.Domain.Entities
{
    public class Ticket : BaseEntity
    {


        public Guid ClassId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Price { get; set; }
        public bool Dispatch { get; set; }
        public Guid BuyId { get; set; }
        public bool Canceled { get; set; }
        public Guid BaggageId { get; set; }

        public Class Class { get; set; }
        public Buy Buy { get; set; }

        public Ticket(Guid classId, string name, string cpf, DateTime birthdate, decimal price, bool dispatch, bool canceled)
        {
            ValidateDomain(classId, name, cpf, birthdate, price, dispatch, canceled);
        }

        public Ticket(Guid classId, string name, string cpf, DateTime birthdate, decimal price, bool dispatch, Guid buyId, bool canceled, Guid baggageId)
        {
            ValidateDomain(classId, name, cpf, birthdate, price, dispatch, canceled);

        }

        private void ValidateDomain(Guid classId, string name, string cpf, DateTime birthdate, decimal price, bool dispatch, bool canceled)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name), "O nome do passageiro deve ser informado ");
            DomainExceptionValidation.When(String.IsNullOrEmpty(cpf), "O CPF do passageiro deve ser informado ");
            DomainExceptionValidation.When(birthdate == DateTime.MinValue, "A data de nascimento do passageiro deve ser informada ");
            DomainExceptionValidation.When(cpf.Length != 11, "O CPF do passageiro está inválido");
            ClassId = classId;
            Name = name;
            Cpf = cpf;
            Birthdate = birthdate;
            Price = dispatch ? price * 1.1m : price;
            Dispatch = dispatch;
            Canceled = canceled;
            BaggageId = dispatch ? Guid.NewGuid() : Guid.Empty;
        }
    }
}

