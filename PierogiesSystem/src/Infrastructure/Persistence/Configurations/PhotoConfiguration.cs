namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    using Domain.ValueObjects;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(e => e.PhotoName).HasMaxLength(128);
            builder.Property(e => e.FileType).HasMaxLength(256);
        }
    }
}