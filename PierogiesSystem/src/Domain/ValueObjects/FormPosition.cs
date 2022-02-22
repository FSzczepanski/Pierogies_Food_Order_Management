namespace CleanArchitecture.Domain.ValueObjects
{
    using System;
    using Enums;

    public class FormPosition
    {
        public Guid PositionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }
        
        public bool HasPhoto { get; set; }
        public Photo? Photo { get; set; }
        
        public PositionCategoryEnum PositionCategory { get; set; }
    }
}