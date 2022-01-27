namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    using Domain.Entities;
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(80);
            builder.Property(e => e.Description).HasMaxLength(200);
            builder.OwnsMany<Location>(p => p.AvailableLocations, n =>
            {
                n.Property(l=>l.Name).HasMaxLength(160);
                n.Property(l=>l.Description).HasMaxLength(300);
                n.Property(l=>l.Street).HasMaxLength(150);
                n.Property(l=>l.ZipCode).HasMaxLength(20);
                n.Property(l=>l.CityName).HasMaxLength(50);
                n.Property(l=>l.CountryName).HasMaxLength(50);
            }).Navigation(n=>n.AvailableLocations);
            
            builder.OwnsMany<FormPosition>(f => f.Positions, n =>
            {
                n.Property(p => p.Name);
                n.Property(p => p.Description);
                n.Property(p => p.Price);
                n.Property(p => p.Vat);
                n.Property(p => p.Amount);
                n.Property(p => p.PortionSize);
            }).Navigation(n=>n.Positions);

            builder.OwnsMany<AvailableDate>(p => p.AvailableDates, n =>
            {
                n.Property(l=>l.From).HasMaxLength(100);
                n.Property(l=>l.To).HasMaxLength(100);
            }).Navigation(n=>n.AvailableDates);

            builder.OwnsOne(x => x.FormActive, n =>
            {
                n.Property(fa => fa.From).HasMaxLength(100);
                n.Property(fa => fa.To).HasMaxLength(100);
            }).Navigation(n=>n.FormActive);
        }
    }
}