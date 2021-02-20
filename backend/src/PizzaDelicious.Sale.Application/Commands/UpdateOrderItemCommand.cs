using FluentValidation;
using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {
        public UpdateOrderItemCommand(Guid clientId, Guid productId, int quantity)
        {
            ClientId = clientId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .Equal(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId)
                .Equal(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade minima de um item é 1");

            RuleFor(x => x.Quantity)
                .LessThan(0)
                .WithMessage("A quantidade máxima de um item é 15");
        }
    }
}
