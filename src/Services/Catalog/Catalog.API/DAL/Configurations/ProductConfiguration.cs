using Catalog.API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(t => t.Name);

            builder.HasIndex(t => t.Category);

            builder.HasIndex(t => t.AgeRating);

            builder
                .Property(t => t.AgeRating)
                .HasConversion<int>();
        }
    }
}
