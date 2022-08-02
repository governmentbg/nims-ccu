using System.Collections.Generic;

namespace Eumis.Public.Data.UmisVOs
{
    public class PPByProcedureChildVO
    {
        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string ProcedureNameAlt { get; set; }

        public string ProcedureCode { get; set; }

        public decimal ContractedEuAmount { get; set; }

        public decimal ContractedBgAmount { get; set; }

        public decimal ContractedSelfAmount { get; set; }

        public decimal ContractedTotalAmount { get; set; }

        public decimal PayedEuAmount { get; set; }

        public decimal PayedBgAmount { get; set; }

        public decimal PayedTotalAmount { get; set; }

        public decimal BfpEuAmount { get; set; }

        public decimal BfpBgAmount { get; set; }

        public decimal BfpTotalmount
        {
            get
            {
                return this.BfpBgAmount + this.BfpEuAmount;
            }
        }
    }
}
