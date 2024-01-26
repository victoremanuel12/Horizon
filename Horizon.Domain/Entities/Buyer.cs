namespace Horizon.Domain.Entities
{
    public class Buyer : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }

        public List<Buy> Buys { get; set; } = new List<Buy>();

    }
}
