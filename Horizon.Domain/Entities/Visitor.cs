using CleanArchMvc.Domain.Validation;

namespace Horizon.Domain.Entities
{
    public class Visitor : BaseEntity
    {


        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }

        public List<Buy> Buys { get; set; } = new List<Buy>();
        public Visitor(string name, string cpf, DateTime birthdate, string email)
        {
            ValidateDomain(name, cpf, birthdate, email);
        }

        private void ValidateDomain(string name, string cpf, DateTime birthdate, string email)
        {
            DomainExceptionValidation.When(String.IsNullOrEmpty(name), "O nome do visitante deve ser informado ");
            DomainExceptionValidation.When(String.IsNullOrEmpty(cpf), "O CPF do visitante deve ser informado ");
            DomainExceptionValidation.When(birthdate == DateTime.MinValue, "A data de nascimento do passageiro deve ser informada ");
            DomainExceptionValidation.When(cpf.Length != 11, "O CPF do visitante está inválido");

            Name = name;
            Cpf = cpf;
            Birthdate = birthdate;
            Email = email;

        }
    }
}
