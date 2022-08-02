using System;

namespace Eumis.Domain.Contracts
{
    public partial class ContractReportFinancialCheck
    {
        public void UpdateAttributes(
            ContractReportFinancialCheckApproval? approval,
            Guid? blobKey,
            DateTime? checkedDate)
        {
            this.Approval = approval;
            this.BlobKey = blobKey;
            this.CheckedDate = checkedDate;

            this.ModifyDate = DateTime.Now;
        }
    }
}
