using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialRevalidation
    {
        public void UpdateAttributes(
            DateTime? revalidationDate,
            Guid? blobKey,
            string notes)
        {
            this.RevalidationDate = revalidationDate;
            this.BlobKey = blobKey;
            this.Notes = notes;

            this.ModifyDate = DateTime.Now;
        }
    }
}
