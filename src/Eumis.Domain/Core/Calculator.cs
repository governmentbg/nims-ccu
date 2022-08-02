using System;

namespace Eumis.Domain.Core
{
    public static class Calculator
    {
        public static decimal RoundBy2(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        public static decimal RoundBy4(decimal value)
        {
            return Math.Round(value, 4, MidpointRounding.AwayFromZero);
        }
    }
}
