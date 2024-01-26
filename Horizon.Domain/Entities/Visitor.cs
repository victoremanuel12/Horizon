using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Horizon.Domain.Entities
{
    public class Visitor : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }

        public List<Buy> Buys { get; set; } = new List<Buy>();
    }
}
