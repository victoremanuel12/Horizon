using CleanArchMvc.Domain.Validation;

namespace Horizon.Domain.Entities
{
    public class Buyer : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }

        public List<Buy> Buys { get; set; } = new List<Buy>();
        public Buyer(Guid id, string name, string cpf, DateTime birthdate)
        {
            ValidateDomain(id, name, birthdate, cpf);

        }
        private void ValidateDomain(Guid id, string name, DateTime birthdate, string cpf)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name), "O nome do comprador deve ser informado ");
            DomainExceptionValidation.When(String.IsNullOrEmpty(cpf), "O CPF do comprador deve ser informado ");
            DomainExceptionValidation.When(birthdate == DateTime.MinValue, "A data de nascimento do comprador deve ser informada ");
            DomainExceptionValidation.When(cpf.Length != 11, "O CPF do comprador está inválido");
            Id = id;
            Name = name;
            Cpf = cpf;
            Birthdate = birthdate;
        }

    }
}
