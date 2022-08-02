using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public partial class ContractReportMicroCheck
    {
        public void UpdateAttributes(ContractReportMicroCheckApproval? approval, DateTime? checkedDate, Guid? blobKey)
        {
            this.AssertIsDraft();
            this.Approval = approval;
            this.CheckedDate = checkedDate;
            this.BlobKey = blobKey;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateCheck(int checkUserId)
        {
            this.AssertIsDraft();

            if (this.CanChangeCStatusToActive().Any())
            {
                throw new DomainException("ContractReportMicroCheck status transition not allowed");
            }

            this.CheckedByUserId = checkUserId;
            this.Status = ContractReportMicroCheckStatus.Active;
            this.ModifyDate = DateTime.Now;
        }

        public void ArchiveCheck()
        {
            this.AssertIsActive();
            this.Status = ContractReportMicroCheckStatus.Archived;
        }

        public IList<string> CanChangeCStatusToActive()
        {
            var errors = new List<string>();

            if (!this.Approval.HasValue)
            {
                errors.Add("Полето 'Одобрение' трябва да е попълнено.");
            }

            if (!this.BlobKey.HasValue)
            {
                errors.Add("Полето 'Файл' трябва да е попълнено.");
            }

            if (!this.CheckedDate.HasValue)
            {
                errors.Add("Полето 'Дата на проверка' трябва да е попълнено.");
            }

            return errors;
        }

        private void AssertIsDraft()
        {
            if (this.Status != ContractReportMicroCheckStatus.Draft)
            {
                throw new DomainException("Cannot edit ContractReportMicroCheck when not in 'Draft' status");
            }
        }

        private void AssertIsActive()
        {
            if (this.Status != ContractReportMicroCheckStatus.Active)
            {
                throw new DomainException("Cannot edit ContractReportMicroCheck when not in 'Active' status");
            }
        }
    }
}
