using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialRevalidationInfoDO
    {
        public ContractReportFinancialRevalidationInfoDO()
        {
        }

        public ContractReportFinancialRevalidationInfoDO(ContractReportFinancialRevalidation contractReportRevalidation)
        {
            this.ContractReportFinancialRevalidationId = contractReportRevalidation.ContractReportFinancialRevalidationId;
            this.ContractReportId = contractReportRevalidation.ContractReportId;
            this.ContractId = contractReportRevalidation.ContractId;
            this.OrderNum = contractReportRevalidation.OrderNum;
            this.Status = contractReportRevalidation.Status;
            this.StatusDescription = contractReportRevalidation.Status;
        }

        public int ContractReportFinancialRevalidationId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportFinancialRevalidationStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportFinancialRevalidationStatus? StatusDescription { get; set; }
    }
}
