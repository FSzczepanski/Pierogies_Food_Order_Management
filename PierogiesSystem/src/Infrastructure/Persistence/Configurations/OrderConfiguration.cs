namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    using AuthorizeNet.APICore;
    using Domain.Entities;
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.Description).HasMaxLength(200);

            builder.OwnsOne<Location>(p => p.Location, n =>
            {
                n.Property(l => l.Name).HasMaxLength(160);
                n.Property(l => l.Description).HasMaxLength(300);
                n.Property(l => l.Street).HasMaxLength(150);
                n.Property(l => l.ZipCode).HasMaxLength(20);
                n.Property(l => l.CityName).HasMaxLength(50);
                n.Property(l => l.CountryName).HasMaxLength(50);
                n.Property(l => l.IsDefault);
            });
            
            builder.OwnsMany<OrderPosition>(f => f.Positions, n =>
            {
                n.Property(p => p.Name);
                n.Property(p => p.Price);
                n.Property(p => p.Vat);
                n.Property(p => p.Amount);
                n.Property(p => p.PortionSize);
            }).Navigation(n=>n.Positions);

            builder.OwnsOne<Purchaser>(f => f.Purchaser, n =>
            {
                n.Property(p=>p.Name).HasMaxLength(150);
                n.Property(p=>p.Email).HasMaxLength(100);
                n.Property(p=>p.Phone).HasMaxLength(50);
            });
            
            builder.OwnsOne<PaymentDetails>(f => f.Payment, n =>
            {
                n.Property(p => p.IsPaid);
                n.Property(p => p.NeedInvoice);
                n.Property(p => p.PaymentDate);
                n.Property(p => p.PaymentMethod);
            });
        }
    }
}