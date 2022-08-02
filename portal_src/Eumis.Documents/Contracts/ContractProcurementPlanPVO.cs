using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractProcurementPlanPVO
    {
        public string contractProcurementPlanName { get; set; }

        public string contractContractNumber { get; set; }

        public DateTime signDate { get; set; }

        public decimal contractContractTotalAmount { get; set; }

        public string contractorName { get; set; }

        public decimal contractReportCSDsTotalAmount { get; set; }

        public int? contractProcurementCheckSheetId { get; set; }

        public List<FinancialCorrectionPVO> financialCorrections { get; set; }
    }

    public class FinancialCorrectionPVO
    {
        public int financialCorrectionId { get; set; }

        public decimal? percent { get; set; }

        public string imposingReason { get; set; }
    }
}
