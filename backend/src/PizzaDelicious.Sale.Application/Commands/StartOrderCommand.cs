using FluentValidation;
using PizzaDelicious.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Application.Commands
{
    public class StartOrderCommand: Command
    {
        public StartOrderCommand(Guid orderId, Guid clientId, decimal total, string cardName, string cardNumber, string cardExpiration, string cardCvv)
        {
            OrderId = orderId;
            ClientId = clientId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardCvv { get; private set; }


        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }


    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(x => x.OrderId)
                .Equal(Guid.Empty)
                .WithMessage("Id do pedido inválido");

            RuleFor(x => x.ClientId)
                .Equal(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.CardName)
                .NotEmpty()
                .WithMessage("O nome do cartão não foi informado");

            RuleFor(x => x.CardNumber)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");

            RuleFor(x => x.CardExpiration)
                .NotEmpty()
                .WithMessage("Data de expiração não informada");

            RuleFor(x => x.CardCvv)
                .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");




        }
    }
}
