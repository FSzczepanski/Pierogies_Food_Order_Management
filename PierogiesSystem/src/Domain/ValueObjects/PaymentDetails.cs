namespace CleanArchitecture.Domain.ValueObjects
{
    using System;
    using Enums;

    public class PaymentDetails
    {
        public PaymentMethodEnum? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool? IsPaid { get; set; }
        public bool? NeedInvoice { get; set; }
    }
}