using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCertCorrectionInfoDO
    {
        public ContractReportFinancialCertCorrectionInfoDO()
        {
        }

        public ContractReportFinancialCertCorrectionInfoDO(ContractReportFinancialCertCorrection contractReportCertCorrection)
        {
            this.ContractReportFinancialCertCorrectionId = contractReportCertCorrection.ContractReportFinancialCertCorrectionId;
            this.ContractReportId = contractReportCertCorrection.ContractReportId;
            this.ContractId = contractReportCertCorrection.ContractId;
            this.OrderNum = contractReportCertCorrection.OrderNum;
            this.Status = contractReportCertCorrection.Status;
            this.StatusDescription = contractReportCertCorrection.Status;
        }

        public int ContractReportFinancialCertCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialCertCorrectionStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCertCorrectionStatus? StatusDescription { get; set; }
    }
}
