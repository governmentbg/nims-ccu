using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCorrection
    {
        public void UpdateAttributes(
            DateTime? correctionDate,
            Guid? blobKey,
            string notes)
        {
            this.CorrectionDate = correctionDate;
            this.BlobKey = blobKey;
            this.Notes = notes;

            this.ModifyDate = DateTime.Now;
        }
    }
}
