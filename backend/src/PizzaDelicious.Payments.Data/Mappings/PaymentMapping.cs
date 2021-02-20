using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaDelicious.Payments.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CardName)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(x => x.CardNumber)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(x => x.CardExpiration)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(x => x.CardCvv)
                .IsRequired()
                .HasColumnType("varchar(4)");

            // 1 : 1 => Payment : Transaction
            builder.HasOne(x => x.Transaction)
                .WithOne(x => x.Payment);

            builder.ToTable("Payments");
        }
    }
}
