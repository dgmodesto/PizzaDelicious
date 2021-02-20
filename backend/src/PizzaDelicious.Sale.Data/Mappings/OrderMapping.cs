using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaDelicious.Sale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasDefaultValue(1001);

            // 1 : N => Order : OrderItems
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.ToTable("Orders");
        }
    }
}
