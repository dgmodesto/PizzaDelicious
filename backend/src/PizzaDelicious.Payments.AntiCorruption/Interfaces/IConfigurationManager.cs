using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaDelicious.Payments.AntiCorruption.Interfaces
{
    public interface IConfigurationManager
    {
        string GetValue(string node);
    }
}
