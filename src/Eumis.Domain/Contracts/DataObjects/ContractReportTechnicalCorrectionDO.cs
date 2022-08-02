using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportTechnicalCorrectionDO
    {
        public ContractReportTechnicalCorrectionDO()
        {
        }

        public ContractReportTechnicalCorrectionDO(
            ContractReportTechnicalCorrection contractReportTechnicalCorrection,
            ContractReportTechnical contractReportTechnical,
            string checkedByUser)
        {
            this.ContractReportTechnicalCorrectionId = contractReportTechnicalCorrection.ContractReportTechnicalCorrectionId;
            this.ContractReportId = contractReportTechnicalCorrection.ContractReportId;
            this.ContractId = contractReportTechnicalCorrection.ContractId;

            this.OrderNum = contractReportTechnicalCorrection.OrderNum;
            this.Status = contractReportTechnicalCorrection.Status;
            this.CorrectionDate = contractReportTechnicalCorrection.CorrectionDate;

            if (contractReportTechnicalCorrection.File != null)
            {
                this.File = new FileDO
                {
                    Key = contractReportTechnicalCorrection.File.Key,
                    Name = contractReportTechnicalCorrection.File.FileName,
                };
            }

            this.Notes = contractReportTechnicalCorrection.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportTechnicalCorrection.CheckedDate;

            if (contractReportTechnical != null)
            {
                this.ContractReportTechnical = new ContractReportTechnicalDO(contractReportTechnical);
            }

            this.CreateDate = contractReportTechnicalCorrection.CreateDate;
            this.ModifyDate = contractReportTechnicalCorrection.ModifyDate;
            this.Version = contractReportTechnicalCorrection.Version;
        }

        public int ContractReportTechnicalCorrectionId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public int OrderNum { get; set; }

        public ContractReportTechnicalCorrectionStatus Status { get; set; }

        public DateTime? CorrectionDate { get; set; }

        public FileDO File { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        public ContractReportTechnicalDO ContractReportTechnical { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
