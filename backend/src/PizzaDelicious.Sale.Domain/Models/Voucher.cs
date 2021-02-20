using FluentValidation;
using FluentValidation.Results;
using PizzaDelicious.Core.DomainObjects;
using PizzaDelicious.Sale.Domain.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Domain.Models
{
    public class Voucher: Entity
    {
        public string Code { get; private set; }
        public decimal? Percent { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public TypeVoucherDiscountEnum TypeVoucherDiscount { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? UseDate { get; private set; }
        public DateTime ValidateDate { get; private set; }
        public bool Enable { get; private set; }
        public bool Used { get; private set; }

        //EF Relationship
        public ICollection<Order> Orders { get; set; }

        internal ValidationResult ValidIfApplicable()
        {
            return new VoucherApplicableValidation().Validate(this);
        }
    }

    public class VoucherApplicableValidation : AbstractValidator<Voucher>
    {
        public VoucherApplicableValidation()
        {
            RuleFor(c => c.ValidateDate)
                .Must(CurrentGreaterThanExpirationDate)
                .WithMessage("Este voucher está expirado");

            RuleFor(c => c.Enable)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido");

            RuleFor(c => c.Used)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Este Voucher não está mais disponível");
        }

        private bool CurrentGreaterThanExpirationDate(DateTime validateDate)
        {
            return validateDate >= DateTime.Now;
        }
    }
}
