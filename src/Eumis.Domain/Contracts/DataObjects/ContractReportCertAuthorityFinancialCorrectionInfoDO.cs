using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertAuthorityFinancialCorrectionInfoDO
    {
        public ContractReportCertAuthorityFinancialCorrectionInfoDO()
        {
        }

        public ContractReportCertAuthorityFinancialCorrectionInfoDO(ContractReportCertAuthorityFinancialCorrection contractCertAuthorityReportCorrection)
        {
            this.ContractReportCertAuthorityFinancialCorrectionId = contractCertAuthorityReportCorrection.ContractReportCertAuthorityFinancialCorrectionId;
            this.ContractReportId = contractCertAuthorityReportCorrection.ContractReportId;
            this.ContractId = contractCertAuthorityReportCorrection.ContractId;
            this.OrderNum = contractCertAuthorityReportCorrection.OrderNum;
            this.Status = contractCertAuthorityReportCorrection.Status;
            this.StatusDescription = contractCertAuthorityReportCorrection.Status;
        }

        public int ContractReportCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportCertAuthorityFinancialCorrectionStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportCertAuthorityFinancialCorrectionStatus? StatusDescription { get; set; }
    }
}
