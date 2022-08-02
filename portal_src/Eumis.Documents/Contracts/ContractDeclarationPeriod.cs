using System;

namespace Eumis.Documents.Contracts
{
    [Serializable]
    public class ContractDeclarationPeriod
    {
        public ContractDeclarationPeriod()
        {
        }

        public ContractDeclarationPeriod(string periodString)
        {
            if (!string.IsNullOrWhiteSpace(periodString))
            {
                var dates = periodString.Split('$');

                if (dates.Length == 2)
                {
                    if (!string.IsNullOrWhiteSpace(dates[0]) && DateTime.TryParse(dates[0], out DateTime dateFrom))
                    {
                        this.DateFrom = dateFrom;
                    }

                    if (!string.IsNullOrWhiteSpace(dates[1]) && DateTime.TryParse(dates[1], out DateTime dateTo))
                    {
                        this.DateTo = dateTo;
                    }
                }
            }
        }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
