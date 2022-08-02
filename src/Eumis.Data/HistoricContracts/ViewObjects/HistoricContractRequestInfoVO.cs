using System;

namespace Eumis.Data.HistoricContract.ViewObjects
{
    public class HistoricContractRequestInfoVO
    {
        public int HistoricContractRequestId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EndDate { get; set; }

        public string StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public int CountContracts { get; set; }

        public string Json { get; set; }
    }
}
