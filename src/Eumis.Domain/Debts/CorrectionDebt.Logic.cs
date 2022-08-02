using System;

namespace Eumis.Domain.Debts
{
    public partial class CorrectionDebt
    {
        public void UpdateAttributes(DateTime regDate, string comment)
        {
            this.AssertIsNotRemoved();

            this.RegDate = regDate;
            this.Comment = comment;

            this.ModifyDate = DateTime.Now;
        }

        public void MakeDeleted(string deleteNote)
        {
            if (this.Status != CorrectionDebtStatus.Entered)
            {
                throw new DomainValidationException("CorrectionDebt status must be Entered in order to mark it as removed.");
            }

            this.Status = CorrectionDebtStatus.Removed;
            this.DeleteNote = deleteNote;

            this.ModifyDate = DateTime.Now;
        }

        public void MakeEntered(string regNumber)
        {
            if (this.Status != CorrectionDebtStatus.New)
            {
                throw new DomainValidationException("CorrectionDebt status must be New in order to enter it.");
            }

            this.Status = CorrectionDebtStatus.Entered;
            this.RegNumber = regNumber;
        }

        public void AssertIsNotRemoved()
        {
            if (this.Status == CorrectionDebtStatus.Removed)
            {
                throw new DomainValidationException("CorrectionDebt status must not be Removed.");
            }
        }
    }
}
