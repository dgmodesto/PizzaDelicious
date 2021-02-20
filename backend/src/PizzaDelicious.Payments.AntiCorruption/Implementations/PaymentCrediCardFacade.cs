using PizzaDelicious.Payments.AntiCorruption.Interfaces;
using PizzaDelicious.Payments.Business.Interfaces;
using PizzaDelicious.Payments.Business.Models;
using PizzaDelicious.Payments.Business.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.AntiCorruption.Implementations
{
    public class PaymentCrediCardFacade : IPaymentCreditCardFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configurationManager;

        public PaymentCrediCardFacade(IPayPalGateway payPalGateway, IConfigurationManager configurationManager)
        {
            _payPalGateway = payPalGateway;
            _configurationManager = configurationManager;
        }

        public Transaction RealizePayment(Order order, Payment payment)
        {
            var apiKey = _configurationManager.GetValue("apiKey");
            var encriptionKey = _configurationManager.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

            var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Value);

            //TODO: o gateway de pagamento que deve retornar o objeto de transação
            var transaction = new Transaction
            {
                OrderId = order.Id,
                Total = order.Value,
                PaymentId = payment.Id
            };

            if(paymentResult)
            {
                transaction.StatusTransaction = StatusTransactionEnum.Paid;
                return transaction;
            }

            transaction.StatusTransaction = StatusTransactionEnum.Refused;
            return transaction;
        }
    }
}
