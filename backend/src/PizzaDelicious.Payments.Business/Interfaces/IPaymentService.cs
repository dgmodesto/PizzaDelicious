using PizzaDelicious.Core.DomainObjects.DTO;
using PizzaDelicious.Payments.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDelicious.Payments.Business.Interfaces
{
    public interface IPaymentService
    {
        Task<Transaction> RealizePaymentOrder(PaymentOrder paymentOrder);
    }
}
