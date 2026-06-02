using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Entities;


namespace OnlineShop.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Patronymic)
                .HasMaxLength(50);


            builder.Property(u => u.NumberPhone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Sex)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(u => u.DateOfBirth)
                .HasColumnType("timestamp without time zone")
                .IsRequired(false);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.AddressID)
                .IsRequired();
        }
    }
}

