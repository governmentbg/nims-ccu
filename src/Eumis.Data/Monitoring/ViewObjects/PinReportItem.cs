using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class PinReportItem
    {
        public string Name { get; set; }

        public string Uin { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Hours { get; set; }

        public string ContractRegNum { get; set; }
    }
}
