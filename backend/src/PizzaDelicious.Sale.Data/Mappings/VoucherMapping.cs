using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaDelicious.Sale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Voucher : Pedidos
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Voucher)
                .HasForeignKey(x => x.VoucherId);

            builder.ToTable("Vouchers");
        }
    }
}
