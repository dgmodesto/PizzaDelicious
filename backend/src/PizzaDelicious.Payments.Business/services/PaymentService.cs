using PizzaDelicious.Core.Communication.Mediator;
using PizzaDelicious.Core.DomainObjects.DTO;
using PizzaDelicious.Core.Messages.CommomMessages.IntegrationEvents;
using PizzaDelicious.Core.Messages.CommomMessages.Notifications;
using PizzaDelicious.Payments.Business.Interfaces;
using PizzaDelicious.Payments.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Payments.Business.services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCreditCardFacade _paymentCreditCardFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentService(IPaymentCreditCardFacade paymentCreditCardFacade, IPaymentRepository paymentRepository, IMediatorHandler mediatorHandler)
        {
            _paymentCreditCardFacade = paymentCreditCardFacade;
            _paymentRepository = paymentRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> RealizePaymentOrder(PaymentOrder paymentOrder)
        {
            var order = new Order
            {
                Id = paymentOrder.OrderId,
                Value = paymentOrder.Total
            };

            var paymento = new Payment
            {
                Value = paymentOrder.Total,
                CardName = paymentOrder.CardName,
                CardNumber = paymentOrder.CardNumber,
                CardExpiration = paymentOrder.CardExpiration,
                CardCvv = paymentOrder.CardCvv,
                Orderid = paymentOrder.OrderId
            };

            var transaction = _paymentCreditCardFacade.RealizePayment(order, paymento);

            if(transaction.StatusTransaction == Models.Enuns.StatusTransactionEnum.Paid)
            {
                paymento.AddEvent(new OrderPaymentRealizedEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, paymentOrder.Total));

                _paymentRepository.Add(paymento);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediatorHandler.PublishEvent(new OrderPaymentRefusedEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, paymentOrder.Total));

            return transaction;
        }
    }
}
