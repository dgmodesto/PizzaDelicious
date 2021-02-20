using FluentValidation;
using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {
        public ApplyVoucherOrderCommand(Guid clientId, string voucherCode)
        {
            ClientId = clientId;
            VoucherCode = voucherCode;
        }

        public Guid ClientId { get; private set; }
        public string VoucherCode { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(x => x.ClientId)
                .Equal(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.VoucherCode)
                .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
                
        }
    }
}
