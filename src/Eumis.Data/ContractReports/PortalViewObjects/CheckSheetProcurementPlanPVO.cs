using System;
using System.Collections.Generic;

namespace Eumis.Data.ContractReports.PortalViewObjects
{
    public class CheckSheetProcurementPlanPVO
    {
        public string ContractProcurementPlanName { get; set; }

        public string ContractContractNumber { get; set; }

        public DateTime SignDate { get; set; }

        public decimal ContractContractTotalAmount { get; set; }

        public string ContractorName { get; set; }

        public decimal ContractReportCSDsTotalAmount { get; set; }

        public int? ContractProcurementCheckSheetId { get; set; }

        public IList<CheckSheetFinancialCorrectionPVO> FinancialCorrections { get; set; }
    }
}
