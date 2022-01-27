namespace CleanArchitecture.Domain.ValueObjects
{
    public class FormPosition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }
    }
}