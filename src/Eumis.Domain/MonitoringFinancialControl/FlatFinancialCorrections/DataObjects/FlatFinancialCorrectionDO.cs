using Eumis.Domain.Core;
using System;

namespace Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects
{
    public class FlatFinancialCorrectionDO
    {
        public FlatFinancialCorrectionDO()
        {
        }

        public FlatFinancialCorrectionDO(FlatFinancialCorrection flatFinancialCorrection)
        {
            this.FlatFinancialCorrectionId = flatFinancialCorrection.FlatFinancialCorrectionId;
            this.ProgrammeId = flatFinancialCorrection.ProgrammeId;
            this.Name = flatFinancialCorrection.Name;
            this.OrderNum = flatFinancialCorrection.OrderNum;
            this.Level = flatFinancialCorrection.Level;
            this.Type = flatFinancialCorrection.Type;
            this.Status = flatFinancialCorrection.Status;
            this.ImpositionDate = flatFinancialCorrection.ImpositionDate;
            this.ImpositionNumber = flatFinancialCorrection.ImpositionNumber;
            this.Description = flatFinancialCorrection.Description;
            this.ContractId = flatFinancialCorrection.ContractId;

            if (flatFinancialCorrection.File != null)
            {
                this.File = new FileDO
                {
                    Key = flatFinancialCorrection.File.Key,
                    Name = flatFinancialCorrection.File.FileName,
                };
            }

            this.CreateDate = flatFinancialCorrection.CreateDate;
            this.ModifyDate = flatFinancialCorrection.ModifyDate;
            this.Version = flatFinancialCorrection.Version;
        }

        public int? FlatFinancialCorrectionId { get; set; }

        public int? ProgrammeId { get; set; }

        public string Name { get; set; }

        public int? OrderNum { get; set; }

        public FlatFinancialCorrectionLevel? Level { get; set; }

        public FlatFinancialCorrectionType? Type { get; set; }

        public FlatFinancialCorrectionStatus Status { get; set; }

        public DateTime? ImpositionDate { get; set; }

        public string ImpositionNumber { get; set; }

        public string Description { get; set; }

        public int? ContractId { get; set; }

        public FileDO File { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
