namespace CleanArchitecture.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Enums;
    using ValueObjects;

    public class Form : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<FormPosition> Positions { get; set; }
        public ICollection<AvailableDate> AvailableDates { get; set; } 
        public List<int> PaymentMethods { get; set; } = new();
        public ICollection<Location> AvailableLocations { get; set; } 
        public AvailableDate FormActive { get; set; } 
        public bool IsActive { get; set; } = false;
        public FormTypeEnum FormType { get; set; }
        public decimal? DeliveryPrice { get; set; } = 0;
        public int PlaceOnList { get; set; }
    }
}