using FluentValidation;
using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public RemoveOrderItemCommand(Guid clientid, Guid productId)
        {
            Clientid = clientid;
            ProductId = productId;
        }

        public Guid Clientid { get; private set; }
        public Guid ProductId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }


    }

    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(x => x.Clientid)
                .Equal(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId)
                .Equal(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }
}
