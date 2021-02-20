using PizzaDelicious.Core.Data;
using PizzaDelicious.Payments.Business.Interfaces;
using PizzaDelicious.Payments.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly PaymentContext _context;

        public PaymentRepository(PaymentContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Payment payment)
        {
            _context.Payments.Add(payment);
        }

        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
