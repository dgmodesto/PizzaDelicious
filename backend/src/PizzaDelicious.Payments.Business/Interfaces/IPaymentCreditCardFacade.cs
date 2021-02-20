using PizzaDelicious.Payments.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Business.Interfaces
{
    public interface IPaymentCreditCardFacade
    {
        Transaction RealizePayment(Order order, Payment payment);
    }
}
