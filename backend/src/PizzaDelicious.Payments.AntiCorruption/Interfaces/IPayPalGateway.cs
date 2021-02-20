using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.AntiCorruption.Interfaces
{
    public interface IPayPalGateway
    {
        string GetPayPalServiceKey(string key, string encriptionKey);
        string GetCardHashKey(string serviceKey, string creditCard);
        bool CommitTransaction(string cardHasKey, string orderId, decimal amount);
    }
}
