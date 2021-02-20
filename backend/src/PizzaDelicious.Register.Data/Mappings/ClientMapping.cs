using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaDelicious.Register.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Register.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.DateOfBirth)
                .IsRequired()
                .HasColumnType("datetime2");


            builder.HasMany(c => c.Addresses)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            builder.ToTable("Clients");

        }
    }
}
