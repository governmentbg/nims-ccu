using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections.DataObjects;
using System;

namespace Eumis.Domain.Debts.DataObjects
{
    public class CorrectionDebtDO
    {
        public CorrectionDebtDO()
        {
        }

        public CorrectionDebtDO(CorrectionDebt correctionDebt, FlatFinancialCorrection flatFinancialCorrection)
        {
            this.CorrectionDebtId = correctionDebt.CorrectionDebtId;
            this.FlatFinancialCorrectionId = correctionDebt.FlatFinancialCorrectionId;
            this.Status = correctionDebt.Status;
            this.RegNumber = correctionDebt.RegNumber;
            this.RegDate = correctionDebt.RegDate;
            this.Comment = correctionDebt.Comment;
            this.IsDeleted = correctionDebt.Status == CorrectionDebtStatus.Removed;
            this.DeleteNote = correctionDebt.DeleteNote;

            this.CreateDate = correctionDebt.CreateDate;
            this.ModifyDate = correctionDebt.ModifyDate;
            this.Version = correctionDebt.Version;

            this.FlatFinancialCorrectionInfo = new FlatFinancialCorrectionInfoDO(flatFinancialCorrection);
        }

        public int? CorrectionDebtId { get; set; }

        public int? FlatFinancialCorrectionId { get; set; }

        public CorrectionDebtStatus? Status { get; set; }

        public string RegNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public string Comment { get; set; }

        public bool? IsDeleted { get; set; }

        public string DeleteNote { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public FlatFinancialCorrectionInfoDO FlatFinancialCorrectionInfo { get; set; }
    }
}
