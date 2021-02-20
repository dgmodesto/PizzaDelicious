using FluentValidation;
using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Commands
{
    public class AddOrderItemCommand: Command
    {
        public AddOrderItemCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitValue)
        {
            ClientId = clientId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(x => x.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(x => x.Quantity)
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(x => x.UnitValue)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");

        }
    }
}
