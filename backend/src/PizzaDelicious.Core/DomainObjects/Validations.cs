using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PizzaDelicious.Core.DomainObjects
{
    public class Validations
    {
        public static void ValidateIfEqualEqual(object object1, object object2, string mensagem)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfDifferent(object object1, object object2, string mensagem)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfDifferent(string pattern, string value, string mensagem)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(value))
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateSize(string value, int Maximum, string mensagem)
        {
            var length = value.Trim().Length;
            if (length > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateSize(string value, int Minimum, int Maximum, string mensagem)
        {
            var length = value.Trim().Length;
            if (length < Minimum || length > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfEmpty(string value, string mensagem)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfNull(object object1, string mensagem)
        {
            if (object1 == null)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateMinimumMaximum(double value, double Minimum, double Maximum, string mensagem)
        {
            if (value < Minimum || value > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateMinimumMaximum(float value, float Minimum, float Maximum, string mensagem)
        {
            if (value < Minimum || value > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateMinimumMaximum(int value, int Minimum, int Maximum, string mensagem)
        {
            if (value < Minimum || value > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateMinimumMaximum(long value, long Minimum, long Maximum, string mensagem)
        {
            if (value < Minimum || value > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateMinimumMaximum(decimal value, decimal Minimum, decimal Maximum, string mensagem)
        {
            if (value < Minimum || value > Maximum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfLessThan(long value, long Minimum, string mensagem)
        {
            if (value < Minimum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfLessThan(double value, double Minimum, string mensagem)
        {
            if (value < Minimum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfLessThan(decimal value, decimal Minimum, string mensagem)
        {
            if (value < Minimum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfLessThan(int value, int Minimum, string mensagem)
        {
            if (value < Minimum)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfFalse(bool boolvalue, string mensagem)
        {
            if (!boolvalue)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidateIfTrue(bool boolvalue, string mensagem)
        {
            if (boolvalue)
            {
                throw new DomainException(mensagem);
            }
        }
    }
}
