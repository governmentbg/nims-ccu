using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCorrectionInfoDO
    {
        public ContractReportFinancialCorrectionInfoDO()
        {
        }

        public ContractReportFinancialCorrectionInfoDO(ContractReportFinancialCorrection contractReportCorrection)
        {
            this.ContractReportFinancialCorrectionId = contractReportCorrection.ContractReportFinancialCorrectionId;
            this.ContractReportId = contractReportCorrection.ContractReportId;
            this.ContractId = contractReportCorrection.ContractId;
            this.OrderNum = contractReportCorrection.OrderNum;
            this.Status = contractReportCorrection.Status;
            this.StatusDescription = contractReportCorrection.Status;
        }

        public int ContractReportFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialCorrectionStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialCorrectionStatus? StatusDescription { get; set; }
    }
}
