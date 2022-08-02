using Eumis.Domain.Contracts.ViewObjects;
using System.Collections.Generic;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportAttachedFinancialCorrectionDO
    {
        public ContractReportAttachedFinancialCorrectionDO()
        {
        }

        public ContractReportAttachedFinancialCorrectionDO(
            ContractReportFinancialCorrection contractReportFinancialCorrection,
            ContractReportFinancial contractReportFinancial,
            ContractReportPayment contractReportPayment,
            string username,
            IList<ContractReportFinancialCorrectionCSDsVO> contractReportFinancialCorrectionCSDs)
        {
            this.ContractReportFinancialCorrection = new ContractReportFinancialCorrectionDO(
            contractReportFinancialCorrection,
            contractReportFinancial,
            contractReportPayment,
            username);

            this.ContractReportFinancialCorrectionCSDs = contractReportFinancialCorrectionCSDs;
        }

        public ContractReportFinancialCorrectionDO ContractReportFinancialCorrection { get; set; }

        public IList<ContractReportFinancialCorrectionCSDsVO> ContractReportFinancialCorrectionCSDs { get; set; }
    }
}
