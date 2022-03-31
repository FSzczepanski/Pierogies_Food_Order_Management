using System;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class OrderPosition
    {
        public Guid? PositionId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }
    }
}