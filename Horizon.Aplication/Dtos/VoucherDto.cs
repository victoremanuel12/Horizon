namespace Horizon.Aplication.Dtos
{
    public class VoucherDto
    {
        public Guid  IdFlight { get; set; }
        public Guid IdTicket { get; set; }
        public string Destiny { get; set; }
        public string Origin { get; set; }
        public string PassengerName { get; set; }
        public string PassengerCpf { get; set; }
        public bool Dispatch { get; set; }


    }
}
