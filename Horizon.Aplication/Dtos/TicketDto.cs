﻿namespace Horizon.Aplication.Dtos
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Dispatch { get; set; }
        public Guid BuyId { get; set; }
        public bool Canceled { get; set; }
        public Guid BaggageId { get; set; } 
    }
}