using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaDelicious.Catalog.Domain.Models;

namespace PizzaDelicious.Catalog.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(c => c.Dimension, cm =>
            {
                cm.Property(c => c.Heigh)
                    .HasColumnName("Heigh")
                    .HasColumnType("int");

                cm.Property(c => c.Width)
                    .HasColumnName("width")
                    .HasColumnType("int");

                cm.Property(c => c.Depth)
                    .HasColumnName("depth")
                    .HasColumnType("int");
            });

            builder.ToTable("Products");
        }
    }
}
