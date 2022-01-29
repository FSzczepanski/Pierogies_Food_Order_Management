namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    using Domain.Entities;
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SystemSettingsConfiguration : IEntityTypeConfiguration<SystemSettings>
    {
        public void Configure(EntityTypeBuilder<SystemSettings> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Nip).HasMaxLength(50);
            builder.Property(e => e.PhoneNumber).HasMaxLength(50);
            builder.Property(e => e.Description).HasMaxLength(500);

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
        }
    }
}