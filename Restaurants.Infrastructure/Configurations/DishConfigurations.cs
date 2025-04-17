using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Configurations;

public class DishConfigurations : IEntityTypeConfiguration<Dish>
{
    public void Configure(EntityTypeBuilder<Dish> builder)
    {
        builder.ToTable("Dishes");

        builder.HasKey(d => d.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(512);
    }
}