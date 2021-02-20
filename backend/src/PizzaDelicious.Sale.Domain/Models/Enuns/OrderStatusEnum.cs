using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Sale.Domain.Models.Enuns
{
    public enum OrderStatusEnum
    {
        Draft = 0,
        Initiate = 1,
        PaidOut = 2,
        Delivered = 3,
        Canceled = 4
    }
}
