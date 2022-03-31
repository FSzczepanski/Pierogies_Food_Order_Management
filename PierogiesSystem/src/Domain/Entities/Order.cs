namespace CleanArchitecture.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Common;
    using ValueObjects;

    public class Order : AuditableEntity
    {
        public Purchaser Purchaser { get; set; }
        public ICollection<OrderPosition> Positions { get; set; }
        public DateTime Date { get; set; }
        public Location Location { get; set; }
        public PaymentDetails? Payment { get; set; }
        public Guid FormId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FullPrice { get; set; }
        
        public string Description { get; set; }
    }
}