using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCertCorrection
    {
        public void UpdateAttributes(
            DateTime? certCorrectionDate,
            Guid? blobKey,
            string notes)
        {
            this.CertCorrectionDate = certCorrectionDate;
            this.BlobKey = blobKey;
            this.Notes = notes;

            this.ModifyDate = DateTime.Now;
        }
    }
}
