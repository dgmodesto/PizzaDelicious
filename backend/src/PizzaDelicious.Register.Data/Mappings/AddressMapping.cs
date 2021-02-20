using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaDelicious.Register.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Register.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Street)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.City)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.State)
                .IsRequired()
                .HasColumnType("varchar(2)");


            builder.ToTable("Addresses");
        }
    }
}
