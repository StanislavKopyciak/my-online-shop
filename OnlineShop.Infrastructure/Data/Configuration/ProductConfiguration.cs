using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Enums;

namespace OnlineShop.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Core.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Delivery)
                .IsRequired();

            builder.Property(p => p.Seller_Id)
                .IsRequired();

            builder.Property(x => x.IsSold)
                .HasDefaultValue(false);

            builder.Property(p => p.Created_At)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder.Property(p => p.Image_Url)
                .HasMaxLength(500);

            builder.Property(p => p.Category)
                .IsRequired();

            builder.HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.AddressID)
                .IsRequired();

        }
    }
}
