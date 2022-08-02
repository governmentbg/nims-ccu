using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportTechnicalCorrectionInfoDO
    {
        public ContractReportTechnicalCorrectionInfoDO()
        {
        }

        public ContractReportTechnicalCorrectionInfoDO(ContractReportTechnicalCorrection contractReportTechnicalCorrection)
        {
            this.ContractReportTechnicalCorrectionId = contractReportTechnicalCorrection.ContractReportTechnicalCorrectionId;
            this.ContractReportId = contractReportTechnicalCorrection.ContractReportId;
            this.ContractId = contractReportTechnicalCorrection.ContractId;
            this.OrderNum = contractReportTechnicalCorrection.OrderNum;
            this.Status = contractReportTechnicalCorrection.Status;
            this.StatusDescription = contractReportTechnicalCorrection.Status;
        }

        public int ContractReportTechnicalCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportTechnicalCorrectionStatus? Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractReportTechnicalCorrectionStatus? StatusDescription { get; set; }
    }
}
