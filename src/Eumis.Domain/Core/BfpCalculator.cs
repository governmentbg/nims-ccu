namespace Eumis.Domain.Core
{
    public static class BfpCalculator
    {
        public static decimal GetBgPercent(decimal euAmount, decimal bgAmount)
        {
            if (euAmount + bgAmount == 0)
            {
                return 0.5m;
            }
            else
            {
                return Calculator.RoundBy4(bgAmount / (euAmount + bgAmount));
            }
        }

        public static decimal GetBgAmount(decimal bfpTotalAmount, decimal bgPercent)
        {
            return Calculator.RoundBy2(bgPercent * bfpTotalAmount);
        }

        public static decimal GetEuAmount(decimal bfpTotalAmount, decimal bgPercent)
        {
            return bfpTotalAmount - GetBgAmount(bfpTotalAmount, bgPercent);
        }

        public static decimal GetBgAmount(decimal bfpTotalAmount, decimal euAmount, decimal bgAmount)
        {
            return Calculator.RoundBy2(GetBgPercent(euAmount, bgAmount) * bfpTotalAmount);
        }

        public static decimal GetEuAmount(decimal bfpTotalAmount, decimal euAmount, decimal bgAmount)
        {
            return bfpTotalAmount - GetBgAmount(bfpTotalAmount, euAmount, bgAmount);
        }
    }
}
