using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Entities;

namespace OnlineShop.Infrastructure.Data.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Core.Entities.Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);
                
            builder.Property(a => a.House)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Apartment)
                .HasMaxLength(50);

              
        }
    }
}
