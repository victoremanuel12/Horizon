﻿namespace Horizon.Aplication.Dtos
{
    public class ClassDto
    {
        public Guid FlightId { get; set; }

        public Guid ClassTypeId { get; set; }
        public int Seats { get; set; }
        public decimal Price { get; set; }
    }
}