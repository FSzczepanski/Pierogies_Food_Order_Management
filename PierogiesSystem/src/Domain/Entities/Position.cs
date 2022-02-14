namespace CleanArchitecture.Domain.Entities
{
    using System;
    using Common;
    using Enums;

    public class Position : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }
        public PositionCategoryEnum PositionCategory { get; set; }

        public bool HasPhoto { get; set; }
    }
}