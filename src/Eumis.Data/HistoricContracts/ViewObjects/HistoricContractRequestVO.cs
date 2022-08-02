using System;

namespace Eumis.Data.HistoricContract.ViewObjects
{
    public class HistoricContractRequestVO
    {
        public int HistoricContractRequestId { get; set; }

        public DateTime CreateDate { get; set; }

        public string StatusCode { get; set; }

        public int CountContracts { get; set; }
    }
}
