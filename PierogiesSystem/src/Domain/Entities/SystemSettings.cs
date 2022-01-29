namespace CleanArchitecture.Domain.Entities
{
    using Common;
    using ValueObjects;

    public class SystemSettings : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Nip { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }
        public int MaxKmFromLocation { get; set; }
        public decimal GlobalDeliveryPrice { get; set; }
    }
}