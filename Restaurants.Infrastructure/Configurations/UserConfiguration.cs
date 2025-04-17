using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Nationality)
            .HasMaxLength(256);

        
        builder.HasMany<Restaurant>(x=>x.OwnedRestaurants).WithOne(x=>x.Owner).HasForeignKey(x => x.OwnerId);
    }
}