using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportRevalidationCertAuthorityFinancialCorrectionInfoDO
    {
        public ContractReportRevalidationCertAuthorityFinancialCorrectionInfoDO()
        {
        }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionInfoDO(ContractReportRevalidationCertAuthorityFinancialCorrection contractCertAuthorityReportCorrection)
        {
            this.ContractReportRevalidationCertAuthorityFinancialCorrectionId = contractCertAuthorityReportCorrection.ContractReportRevalidationCertAuthorityFinancialCorrectionId;
            this.ContractReportId = contractCertAuthorityReportCorrection.ContractReportId;
            this.ContractId = contractCertAuthorityReportCorrection.ContractId;
            this.OrderNum = contractCertAuthorityReportCorrection.OrderNum;
            this.Status = contractCertAuthorityReportCorrection.Status;
            this.StatusDescription = contractCertAuthorityReportCorrection.Status;
        }

        public int ContractReportRevalidationCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportRevalidationCertAuthorityFinancialCorrectionStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportRevalidationCertAuthorityFinancialCorrectionStatus? StatusDescription { get; set; }
    }
}
