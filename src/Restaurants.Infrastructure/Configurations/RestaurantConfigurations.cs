using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Configurations;

public class RestaurantConfigurations : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurants");

        builder.HasKey(r => r.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(512);

        builder.HasMany(x => x.Dishes)
            .WithOne()
            .HasForeignKey(x => x.RestaurantId);

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.City).IsRequired().HasMaxLength(50);
            addressBuilder.Property(x => x.Street).IsRequired().HasMaxLength(150);
            addressBuilder.Property(x => x.PostalCode).IsRequired().HasMaxLength(10);
        });
    }
}